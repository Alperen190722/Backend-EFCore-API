
import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DepartmentService } from '../../../services/department.service'; 
import { RoleService } from '../../../services/role.service'; 
import { Department } from '../../../models/department/department';
import { Role } from '../../../models/role/role';

@Component({
  selector: 'app-department-role-management',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './department-role-management.html',
  styleUrl: './department-role-management.scss'
})
export class DepartmentRoleManagement implements OnInit {
  private deptService = inject(DepartmentService);
  private roleService = inject(RoleService);
  roles: any[] = [];

departments = signal<Department[]>([]);
  filteredDepartments = signal<Department[]>([]);
  selectedRoles = signal<Role[]>([]);
  selectedDeptId = signal<number | null>(null);
  selectedDeptName = signal<string>('');
  searchTerm = '';

  ngOnInit() { this.loadDepartments(); }

  loadDepartments() {
    this.deptService.getDepartments().subscribe(data => {
      this.departments.set(data);
      this.filteredDepartments.set(data);
    });
  }

  
  filterDepartments() {
    const term = this.searchTerm.toLowerCase();
    const filtered = this.departments().filter(d => 
      d.Name?.toLowerCase().includes(term) || 
      d.RoleNames?.some(r => r.toLowerCase().includes(term))
    );
    this.filteredDepartments.set(filtered);
  }

  selectDepartment(deptId: number) {
    const dept = this.departments().find(d => d.Id === deptId);
    this.selectedDeptName.set(dept?.Name || '');
    this.selectedDeptId.set(deptId);
    this.roleService.getRolesByDepartment(deptId).subscribe(roles => this.selectedRoles.set(roles));
  }


deleteDept(id: number) {
  if(confirm("Bu departmanı silmek istediğinize emin misiniz?")) {
    this.deptService.deleteDepartment(id).subscribe({
      next: () => {


        this.departments.set(this.departments().filter(d => d.Id !== id));
        this.filterDepartments(); 
        alert("Departman silindi!");
      },
      error: (err) => alert("Silme başarısız: " + err.message)
    });
  }
}

deleteRole(id: number) {
  if(confirm("Bu rolü silmek istiyor musunuz?")) {
    this.roleService.deleteRole(id).subscribe({
      next: () => {

        this.selectedRoles.set(this.selectedRoles().filter(r => r.Id !== id));
        alert("Rol Başarıyla Silindi!");
      },
      error: (err) => alert("Hata! " + err.message)
    });
  }
}

  updateRole(role: Role) {
    this.roleService.updateRole(role).subscribe({
      next: () => alert("Rol başarıyla Güncellendi!"),
      error: (err) => alert("Hata! " + err.message)
    });
  }



openDeptModal() {
  const name = prompt("Yeni Departman Adı:");
  if (name) {
    const newDept: Department = { Name: name, IsActive: true };
    this.deptService.addDepartment(newDept).subscribe(() => {
      this.loadDepartments(); 
      alert("Departman Eklendi!");
    });
  }
}


editDept(dept: Department) {
  const newName = prompt("Departman adını güncelle:", dept.Name);
  if (newName) {
    dept.Name = newName;
  }
}

openRoleModal() {
  const name = prompt("Yeni Rol Adı:");
  if (!name) return;

  const minSalaryStr = prompt("Minimum Maaş (0'dan büyük olmalı):", "25000");
  const maxSalaryStr = prompt("Maximum Maaş (Min'den büyük olmalı):", "45000");

  const minSalary = Number(minSalaryStr);
  const maxSalary = Number(maxSalaryStr);


  if (minSalary <= 0 || maxSalary <= 0) {
    alert("Maaşlar 0'dan büyük olmalıdır!");
    return;
  }

  const deptId = this.selectedDeptId();
  if (deptId) {
    const newRole: Role = { 
      Name: name, 
      DepartmentId: deptId, 
      MinSalary: minSalary, 
      MaxSalary: maxSalary,
      IsActive: true,
      DepartmentName: this.selectedDeptName() 
    };

    this.roleService.addRole(newRole).subscribe({
      next: () => {
        this.selectDepartment(deptId); 
        alert(`${name} rolü listeye eklendi!`);
      },
      error: (err) => {
        alert((err.error?.message || "Maaş kurallarına uyunuz!"));
      }
    });
  }
}
}
