import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TerminationService } from '../../services/termination.service';
import { NotifService } from '../../services/notif.service';
import { Termination } from '../../models/termination/termination';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-hr-panel',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './hr-panel.component.html',
  styleUrls: ['./hr-panel.component.scss']
})
export class HrPanelComponent implements OnInit {
  employees: Termination[] = [];
  loading: boolean = false;

  constructor(
    private terminationService: TerminationService,
    private notif: NotifService,
    private cdr: ChangeDetectorRef,
    private router: Router
  ) {}

  ngOnInit(): void {
    const token = sessionStorage.getItem('skylineToken');
    const dept = sessionStorage.getItem('Department');

    if (!token || dept?.toLowerCase() !== 'human resources') {
      this.router.navigate([!token ? '/login' : '/dashboard']);
      return;
    }

    this.loadData();
  }

  loadData() {
    if (this.loading) return;
    this.loading = true;
    this.terminationService.getAllHistory().subscribe({
      next: (res: any) => {
        const actualData = res?.Data || res?.data || (Array.isArray(res) ? res : []);
        
        if (actualData && Array.isArray(actualData)) {
          this.employees = actualData.filter((t: any) => t.Status === 'ManagerApproved');
        }
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.notif.error("Veriler çekilemedi!");
        this.loading = false;
      }
    });
  }

  
approveHR(term: Termination) {
  const targetId = term.EmployeeId;


  this.employees = [...this.employees.filter(e => e.EmployeeId !== targetId)];
  

  this.cdr.detectChanges(); 

  this.terminationService.approveHR(targetId).subscribe({
    next: () => {

      this.notif.success("İşlem tamam, personel listeden kaldırıldı.");
    },
    error: () => {
      this.notif.error("Hata oldu, personel geri getiriliyor.");
      this.loadData(); 
    }
  });
}
}