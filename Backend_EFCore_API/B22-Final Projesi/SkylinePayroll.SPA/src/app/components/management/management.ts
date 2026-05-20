import { Component, inject, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EmployeeService } from '../../services/employee.service';
import { Employee } from '../../models/employee/employee';
import { TerminationService } from '../../services/termination.service';
import { NotifService } from '../../services/notif.service';
import { Resignation } from '../../models/resignation/resignation';
import { EmployeeStatus } from '../../models/enums/employee.enum';
import { EmployeeUpdate } from '../../models/employeeupdate/employeeupdate';

@Component({
  selector: 'app-management',
  standalone: true,
  imports: [RouterModule, CommonModule, FormsModule],
  templateUrl: './management.html',
  styleUrl: './management.scss',
})
export class Management implements OnInit {
  private router = inject(Router);
  private employeeService = inject(EmployeeService);
  private terminationService = inject(TerminationService);
  private notif = inject(NotifService);
  private cdr = inject(ChangeDetectorRef);
  EmployeeStatus = EmployeeStatus;

  employees: Employee[] = [];

  selectedEmployee: any | null = null; 
  loading: boolean = true;
  resignationReason: string = '';

  departments: any[] = [];
  roles: any[] = [];

  toNum(value: any): number {
    if (!value) return 0;
    if (typeof value === 'number') return value;

    return Number(value.toString().replace(' TL', '').replace(/\./g, '').replace(',', '.'));
  }

  ngOnInit(): void {
    const token = sessionStorage.getItem('skylineToken');
    const dept = sessionStorage.getItem('Department');

    if (!token || dept?.toLowerCase() !== 'management') {
      this.router.navigate([!token ? '/login' : '/dashboard']);
      return;
    }

    this.notif.notifications$.subscribe((notifs) => {
      this.loadEmployees(); 
    });

    this.loadEmployees();
    this.loadStaticData(); 
    this.notif.refreshNotifications();
  }


  loadStaticData() {
   this.employeeService.getDepartments().subscribe((data: any[]) => {
    this.departments = data;
  });
  
  this.employeeService.getRoles().subscribe((data: any[]) => {
    this.roles = data;
  });
  }



get selectedRoleRange() {
  if (!this.selectedEmployee || !this.roles.length) return null;


  const role = this.roles.find(r => Number(r.Id || r.id) === Number(this.selectedEmployee?.RoleId));

  if (role) {

    return { 
      min: role.MinSalary ?? role.minSalary, 
      max: role.MaxSalary ?? role.maxSalary, 
      name: role.Name ?? role.name 
    };
  }
  return null;
}

  loadEmployees() {
    this.loading = true;
const myId = Number(sessionStorage.getItem('EmployeeId'));
 const myRole = (sessionStorage.getItem('Role') || '').toString().trim().toLowerCase();
 const myLevel = (sessionStorage.getItem('HierarchyLevel') || 0);

  this.employeeService.getEmployees().subscribe({
    next: (data: any[]) => {

      this.employees = data.filter(emp => {
        const targetId = Number(emp.Id || emp.id);
        const targetRole = (emp.Role || emp.role || '').toString().trim().toLowerCase();
        const targetLevel = (emp.HierarchyLevel || emp.hierarchyLevel || 0);



        if (targetRole === 'boss' && myRole !== 'boss') {
          return false;
        }



        if (targetId === myId) {
          return false;
        }

        if (myLevel <= targetLevel) {
          return false;
        }

        return true;
        }).map((emp) => {

          let statusValue = Number(emp.Status ?? emp.status);
          const isManagement = emp.DepartmentName?.toLowerCase() === 'management';

          if (statusValue === 4 && isManagement) statusValue = 3;

          let rawSalary = emp.Salary ?? emp.salary ?? 0;
          if (typeof rawSalary === 'string') {
            rawSalary = rawSalary.replace(' TL', '').replace(/\./g, '').replace(',', '.');
          }

          return { 
            ...emp, 
            Status: statusValue,
            Salary: isNaN(Number(rawSalary)) ? 0 : Number(rawSalary)
          };
        });
        
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.notif.error('Personel verileri hiyerarşik kontrolden geçemedi!');
        this.loading = false;
      }
    });
  }



onDepartmentChange(deptId: any) {
  if (!deptId) return;
  
  const id = Number(deptId);
  this.employeeService.getRoles(id).subscribe({
    next: (data: any[]) => {
      this.roles = data; 

      this.selectedEmployee.RoleId = null;
    },
    error: () => this.notif.error("Bu departmana ait roller çekilemedi!")
  });
}

selectEmployee(emp: Employee) {


  const tempEmp = emp as any; 
  
  this.selectedEmployee = { ...tempEmp };



  const deptId = tempEmp.DepartmentId || tempEmp.departmentId;
  
  if (deptId) {
    this.onDepartmentChange(deptId);
  }
}

 onPromote() {
  if (!this.selectedEmployee) return;


  let fName = this.selectedEmployee.FirstName || this.selectedEmployee.firstName;
  let lName = this.selectedEmployee.LastName || this.selectedEmployee.lastName;
  let fullName = this.selectedEmployee.FullName || this.selectedEmployee.fullName || 'Personel';

  
  if (!fName && fullName !== 'Personel') {
    const parts = fullName.trim().split(' ');
    lName = parts.pop(); 
    fName = parts.join(' '); 
  }


  const updateDto = {
    Id: Number(this.selectedEmployee.Id || this.selectedEmployee.id),
    FirstName: fName, 
    LastName: lName,
    Salary: Number(this.selectedEmployee.Salary ?? this.selectedEmployee.salary ?? 0),
    DepartmentId: Number(this.selectedEmployee.DepartmentId || this.selectedEmployee.departmentId),
    RoleId: Number(this.selectedEmployee.RoleId || this.selectedEmployee.roleId),
    Status: Number(this.selectedEmployee.Status ?? this.selectedEmployee.status), 
    Iban: this.selectedEmployee.Iban || this.selectedEmployee.iban || "TR000000000000000000000000"
  };


  const confirmMessage = `${fullName} için hazırlanan dosyayı (Terfi/Güncelleme) mühürlemek istediğinize emin misiniz?`;

  if (confirm(confirmMessage)) {
    this.loading = true;
    this.employeeService.updateEmployee(updateDto).subscribe({
      next: () => {
        this.notif.success(`${fullName} terfi/güncelleme harekatı başarıyla tamamlandı!`);
        this.loadEmployees();
        this.selectedEmployee = null;
      },
      error: (err) => {
        console.error("Hata:", err);
        
        this.notif.error('Güncelleme yapılamadı. Veri paketinde eksik veya rota hatası mevcut!');
        this.loading = false;
      }
    });
  }
}
onApproveResignation(emp: any) {
  const employeeId = emp.Id || emp.id || (emp as any).EmployeeId;
  const fullName = emp.FullName || 'Personel';

  if (confirm(`${fullName} isimli personelin istifasını onaylıyor musunuz?`)) {
    this.loading = true; 
    
    this.terminationService.approveResignation(Number(employeeId)).subscribe({
      next: (res: any) => {


        
        this.notif.success(`${fullName} istifası onaylandı.`);
        

        this.loadEmployees(); 
        this.loading = false;
      },
      error: (err) => {


        console.error("Hata:", err);
        this.notif.error("Onay sırasında yetki sorunu oluştu.");
        this.loading = false;
      }
    });
  }
}

onSubmitMyResignation() {
  const rawId = sessionStorage.getItem('EmployeeId');
  if (!rawId) return;

  if (confirm('Yönetimden ayrılmak istediğinize emin misiniz? Bu işlem Dashboard ve tüm kayıtlarda statünüzü "AYRILDI" olarak güncelleyecektir.')) {
    const resignationData: Resignation = {
      employeeId: Number(rawId),
      reason: this.resignationReason,
      departmentId: Number(sessionStorage.getItem('DepartmentId')),
    };


    this.terminationService.submitResignation(resignationData).subscribe({
      next: () => {
        


        this.employeeService.updateStatus(Number(rawId), 3).subscribe({
          next: () => {
            this.notif.success('İstifa sunuldu ve veritabanı mühürlendi. Dashboard artık "AYRILDI" gösteriyor.');
            this.loadEmployees(); 
            this.resignationReason = '';
          },
          error: (err) => {
            console.error("Hata:", err);
            this.notif.error('İstifa sunulamadı!');
          }
        });
      },
      error: () => this.notif.error('İstifa süreci başlatılamadı!')
    });
  }
}
  onTerminate() {
  if (!this.selectedEmployee?.Id) return;


  const reason = prompt(`${this.selectedEmployee.FullName} için fesih sebebi belirtiniz:`, 'Performans yetersizliği');


  if (reason === null) return; 

  if (reason.trim().length < 5) {
    this.notif.error('Lütfen daha açıklayıcı bir sebep yazınız (en az 5 karakter).');
    return;
  }


  this.terminationService.initiate(this.selectedEmployee.Id, reason).subscribe({
    next: () => {
      this.notif.success('Fesih dosyası İK birimine iletildi.');
      this.loadEmployees();
      this.selectedEmployee = null;
    },
    error: (err) => this.notif.error(err.error?.message || 'Hata oluştu.')
  });
}

  getStatusLabel(status: any): string {
    const s = Number(status);
    switch (s) {
      case 1: return 'AKTİF';
      case 2: return 'FESİH SÜRECİ';
      case 3: return 'AYRILDI';
      case 4: return 'İSTİFA TALEBİ';
      default: return `BELİRSİZ (${status})`;
    }
  }

  getStatusClass(status: any): string {
    const s = status?.toString();
    if (s === '1') return 'badge-active';
    if (s === '2') return 'badge-termination';
    if (s === '3') return 'badge-passive';
    if (s === '4') return 'badge-resignation';
    return 'badge-default';
  }
}