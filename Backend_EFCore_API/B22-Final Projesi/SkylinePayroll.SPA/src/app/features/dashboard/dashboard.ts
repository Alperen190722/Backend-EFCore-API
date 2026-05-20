import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router'; 
import { EmployeeService } from '../../services/employee.service'; 
import { Employee } from '../../models/employee/employee';
import { AuthService } from '../../services/auth.service';
import { EmployeeStatus } from '../../models/enums/employee.enum';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard implements OnInit {
  private employeeService = inject(EmployeeService);
  private cdr = inject(ChangeDetectorRef);
  private router = inject(Router); 
  private authService = inject(AuthService);
  

  public EmployeeStatus = EmployeeStatus;

  employees: Employee[] = [];


  toNum(value: any): number {
    return value !== null && value !== undefined ? Number(value) : 0;
  }

  ngOnInit(): void {
    const token = sessionStorage.getItem('skylineToken'); 
    if (!token) {
      this.router.navigate(['/login']);
      return;
    }
    this.loadEmployees(); 
  }

  /**
   * 🕵️ VALIDASYON: 
   * Gelen verinin sayısal bir statü olup olmadığını denetler.
   */
  isStatusValid(status: any): boolean {
    return status !== null && status !== undefined && !isNaN(Number(status));
  }

  /**
   * 📋 LABEL MÜHRÜ: 
   * Enum değerlerini insan diline çevirir. 
   * EmployeeStatus (4) personelin kaderini burada belirler.
   */
  getStatusLabel(status: any): string {
    const s = this.toNum(status);
    
    switch (s) {
      case this.EmployeeStatus.Active: return 'AKTİF';
      case this.EmployeeStatus.PendingTermination: return 'FESİH SÜRECİ';
      case this.EmployeeStatus.Passive: return 'AYRILDI';
      case this.EmployeeStatus.PendingResignation: return 'İSTİFA TALEBİ'; 
      default: return 'BELİRSİZ';
    }
  }

  /**
   * 🚀 VERİ OPERASYONU: 
   * Backend'den gelen veriyi "erkek gibi" ayıklar, 
   * tip dönüşümlerini yapar ve listeyi mühürler.
   */
  loadEmployees() {
    this.employeeService.getEmployees().subscribe({
      next: (data: any[]) => {
        this.employees = (data || []).map(emp => {

          let rawStatus = emp.status ?? emp.Status; 
          let finalStatus: number;


          if (typeof rawStatus === 'number') {
            finalStatus = rawStatus; 
          } 

          else if (typeof rawStatus === 'string') {
            const s = rawStatus.toLowerCase().trim();
            
            if (s.includes('resignation') || s.includes('istifa')) {
                finalStatus = this.EmployeeStatus.PendingResignation; 
            } else if (s.includes('termination') || s.includes('fesih')) {
                finalStatus = this.EmployeeStatus.PendingTermination; 
            } else if (s.includes('passive') || s.includes('ayrildi')) {
                finalStatus = this.EmployeeStatus.Passive; 
            } else {
                finalStatus = this.EmployeeStatus.Active; 
            }
          } else {
            finalStatus = this.EmployeeStatus.Active;
          }

          return { 
            ...emp, 
            Status: finalStatus 
          };
        });
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error("Dashboard yüklenemedi:", err);
      }
    });
  }

  onLogoutClick() {
    this.authService.logOut(); 
  }
}