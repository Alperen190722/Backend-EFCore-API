import { Component, OnInit, ChangeDetectorRef, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TerminationService } from '../../services/termination.service';
import { NotifService } from '../../services/notif.service';
import { FormsModule } from '@angular/forms';
import { Resignation } from '../../models/resignation/resignation';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-employee-termination-panel',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './employee-termination-panel.html',
  styleUrls: ['./employee-termination-panel.scss'],
})
export class EmployeeTerminationPanel implements OnInit {
  detail: any = null;
  loading: boolean = false;
  resignationReason: string = '';
  userEmpStatus: number = 1; 
  
  private cdr = inject(ChangeDetectorRef);
  private router = inject(Router);

  isProcessing: boolean = false; 

  constructor(
    private terminationService: TerminationService,
    private notif: NotifService,
  ) {}

  ngOnInit(): void {
    const token = sessionStorage.getItem('skylineToken');
    const dept = sessionStorage.getItem('Department')?.toLowerCase();
    
    
    this.userEmpStatus = Number(sessionStorage.getItem('Status') || 1);

    if (!token) {
      this.router.navigate(['/login']);
      return;
    }

    const employeeId = Number(sessionStorage.getItem('EmployeeId'));


  if (dept === 'management') {

    this.terminationService.getDetailForEmployee(employeeId).subscribe({
      next: (res: any) => {
        const actualData = res?.data || res?.Data;
        


        if (!actualData || (actualData.Status !== "2" && actualData.status !== "2")) {
          this.notif.error('Yönetim kadrosu bu paneli sadece fesih onay süreci için kullanabilir!');
          this.router.navigate(['/management']);
        } else {

          this.loadDetail(employeeId);
        }
      },
      error: () => {
        this.router.navigate(['/management']);
      }
    });
    return; 
  }
    if (employeeId) {
      this.loadDetail(employeeId);
    }
  }

loadDetail(id: number) {
  this.loading = true;
  this.terminationService.getDetailForEmployee(id).subscribe({
    next: (res: any) => {
      const actualData = res?.data || res?.Data;
      
      if (actualData) {

        this.detail = {
          employeeId: actualData.EmployeeId ?? actualData.employeeId ?? id, 
          calculatedAmount: actualData.TotalAmount ?? actualData.CalculatedAmount ?? actualData.calculatedAmount ?? 0,
          iban: actualData.Iban ?? actualData.iban ?? 'Belirtilmemiş',
          reason: actualData.Reason ?? actualData.reason,
          status: actualData.Status ?? actualData.status 
        };
      } else {
        this.detail = null;
      }
      this.loading = false;
      this.cdr.detectChanges();
    },
    error: () => {
      this.loading = false;
      this.cdr.detectChanges();
    }
  });
}

  onResign() {

    if (this.userEmpStatus !== 1) {
      this.notif.error("Aktif olmayan personel istifa talebi oluşturamaz.");
      return;
    }

    const empId = Number(sessionStorage.getItem('EmployeeId'));
    const userDeptId = Number(sessionStorage.getItem('DepartmentId'));

    if (!this.resignationReason || this.resignationReason.length < 10) {
      this.notif.error("Lütfen en az 10 karakterlik bir sebep belirtiniz.");
      return;
    }

    this.loading = true;
    const resignationData: Resignation = {
      employeeId: empId,
      reason: this.resignationReason,
      departmentId: userDeptId
    };

    this.terminationService.submitResignation(resignationData).subscribe({
      next: () => {
        this.notif.success("İstifa talebiniz iletildi.");
        this.resignationReason = '';
        this.notif.refreshNotifications(); 
        this.loadDetail(empId);
      },
      error: (err) => {
        const errorMessage = err?.error?.Message || err?.error?.message || "İstifa talebi oluşturulamadı.";
        this.notif.error(errorMessage);
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }

 onApprove() {

  if (this.isProcessing) return; 
  

  this.isProcessing = true; 
  this.cdr.detectChanges(); 

  const targetId = this.detail?.employeeId || Number(sessionStorage.getItem('EmployeeId'));
  
  if (!targetId) {
    this.isProcessing = false;
    this.notif.error("Kimlik bulunamadı.");
    return;
  }

  this.terminationService.employeeApprove(targetId).subscribe({
    next: (res: any) => {
      if (res.success || res.Success) {
        this.notif.success("Onaylandı.");



        if(this.detail) this.detail.status = 3; 
      }
      this.isProcessing = false;
      this.cdr.detectChanges();
    },
    error: (err) => {

      if (err.status === 400 || err.error?.Message?.includes("pending")) {
        this.notif.success("İşleminiz başarıyla sisteme işlendi."); 
         if(this.detail) this.detail.status = 3;
      } else {
         this.notif.error("İşlem sırasında bir hata oluştu.");
      }
      this.isProcessing = false;
      this.cdr.detectChanges();
    }
  });
}
}