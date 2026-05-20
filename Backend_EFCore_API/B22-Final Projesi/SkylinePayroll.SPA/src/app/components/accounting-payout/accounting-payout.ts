import { Component, OnInit, inject, ChangeDetectorRef, AfterContentChecked } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TerminationService } from '../../services/termination.service';
import { ToastrService } from 'ngx-toastr';
import { Router, RouterModule } from '@angular/router';

@Component({
selector:'app-accounting-payout',
standalone:true,
imports:[CommonModule,RouterModule],
templateUrl:'./accounting-payout.html',
styleUrls:['./accounting-payout.scss']
})

export class AccountingPayout implements OnInit {
  pendingPayments: any[] = [];
  loading = false;
  
  private cdr = inject(ChangeDetectorRef);
  private terminationService = inject(TerminationService);
  private toastr = inject(ToastrService);
  private router = inject(Router);

  ngOnInit(): void {
    const token = sessionStorage.getItem('skylineToken');
    const dept = sessionStorage.getItem('Department');

    
    if (!token || (dept?.toLowerCase() !== 'accounting' && dept?.toLowerCase() !== 'accountant')) {
      this.toastr.warning("Buraya girmek için  yetkiniz yetersiz!");
      this.router.navigate([!token ? '/login' : '/dashboard']);
      return;
    }
    this.fetchPayments();
  }

  fetchPayments() {
    setTimeout(() => {
      this.loading = true;
      this.cdr.detectChanges();
    });

    this.terminationService.getPendingAccountingPayments().subscribe({
      next: (res: any) => {
        const list = res.Data || res.data || (Array.isArray(res) ? res : null);
        this.pendingPayments = Array.isArray(list) ? list : [];
        

        if (this.pendingPayments.length === 0) {

        }

        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.toastr.error('Bağlantıda bir kopukluk var.');
        this.loading = false;
        this.cdr.detectChanges();
      },
    });
  }


  onPay(employeeId: number, terminationType: number = 1) {
    if (!employeeId) {
      this.toastr.error('Personel bilgisi bulunamadı!');
      return;
    }

    this.toastr.clear();

    this.terminationService.finalize(employeeId, terminationType).subscribe({
      next: (res: any) => {
        if (res.Success || res.success) {
          this.toastr.success('Ödeme onaylandı, personel pasife çekildi.', 'Mührü', {
            timeOut: 2000,
            progressBar: true
          });
          

          this.fetchPayments(); 
        } else {

          this.toastr.error(res.Message || res.message || 'İşlem başarısız.');
        }
      },
      error: (err) => {

        if(err.status === 401 || err.status === 403) {
          this.toastr.error('Bu ödemeyi yapmaya yetkiniz yok!');
        } else {
          this.toastr.error('Ödeme onaylanırken bir sorun oluştu!');
        }
      },
    });
  }
}