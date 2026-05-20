import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PayrollService } from '../../services/payroll.service';
import { ToastrService } from 'ngx-toastr';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-payroll',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './payroll.html', 
  styleUrl: './payroll.scss'
})
export class Payroll implements OnInit {
  private payrollService = inject(PayrollService);
  private toastr = inject(ToastrService);
  private router = inject(Router);

  constructor(private cdr: ChangeDetectorRef){}


  summary: any = null;
  isLoading = false;

  ngOnInit(): void {

    const token = sessionStorage.getItem('skylineToken');
    const dept = sessionStorage.getItem('Department');

    if (!token || dept?.toLowerCase() !== 'accounting') {
      this.router.navigate([!token ? '/login' : '/dashboard']);
      return;
    }
    this.loadSummary();
}

  loadSummary() {
    this.isLoading = true;
    this.payrollService.getDashboardSummary().subscribe({
      next: (res: any) => {
        if (res.success) {
          this.summary = res.data;
        }
        this.isLoading = false;
        this.cdr.detectChanges(); 
      },
      error: (err) => {
        this.toastr.error('Mali özet yüklenirken hata oluştu!');
        this.isLoading = false;
      }
    });
  }

executePayroll() {
  if (this.summary && this.summary.pendingPayrolls === 0) {
    this.toastr.error('Maaşlar zaten ödendi!');
    return;
  }

  if (!confirm('Nisan 2026 ödemelerini onaylıyor musunuz?')) return;


  this.isLoading = true;
  this.cdr.detectChanges(); 

  this.payrollService.runMonthlyPayroll().subscribe({
    next: (res: any) => {

      if (res.success) {
        this.toastr.success(res.message || "Ödeme başarıyla tamamlandı.");
        

        this.summary = { 
          ...this.summary, 
          pendingPayrolls: 0 
        };
      } else {
        this.toastr.warning(res.message);
      }
      

      this.isLoading = false;
      this.cdr.detectChanges(); 
    },
    error: (err) => {
      const serverMessage = err.error?.Message || err.error?.message || "Hata oluştu!";
      this.toastr.error(serverMessage);
      

      this.isLoading = false;
      this.cdr.detectChanges();
    }
  });
}
}