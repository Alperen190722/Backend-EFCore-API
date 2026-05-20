import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router'; 
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, FormsModule],
  templateUrl: './register.html',
  styleUrl: './register.scss',
})
export class Register implements OnInit {
  registerForm!: FormGroup;
  departments: any[] = []; 
  roles: any[] = [];        

  constructor(
    private fb: FormBuilder, 
    private authService: AuthService, 
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {

const token = sessionStorage.getItem('skylineToken');

  const dept = sessionStorage.getItem('Department')?.toLowerCase(); 



  if (!token || (dept !== 'management' && dept !== 'human resources')) {
    console.warn("Yetkisiz Giriş Girişimi:", dept);
    this.router.navigate([!token ? '/login' : '/dashboard']);
    return;
  }

    this.registerForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      identityNumber: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      phoneNumber: ['', [Validators.required]],
      salary: [0, [Validators.required]],
      gender: ['', [Validators.required]],
      departmentId: ['', [Validators.required]],
      roleId: ['', [Validators.required]],
      iban: ''
    });

    this.loadDepartments(); 
  }

  loadDepartments() {
    this.authService.getDepartments().subscribe({
    next: (data) => {
      this.departments = data;
      this.cdr.detectChanges();
    },
    error: (err) => console.error('Departman yüklenemedi!:', err)
  });
}


  onDepartmentChange(event: any) {

  const target = event.target as HTMLSelectElement; 
  const deptId = target.value;


  if (deptId) {
    this.authService.getRolesByDepartment(Number(deptId)).subscribe({
      next: (roles) => {
        this.roles = roles;
        this.cdr.detectChanges();
      },
      error: (err) => console.error('Roller yüklenemedi!:', err)
    });
  }
  }

  selectedRole: any = null; 

onRoleChange(event: any) {
  const target = event.target as HTMLSelectElement;
  const roleId = Number(target.value);
  

  this.selectedRole = this.roles.find(r => r.Id === roleId);
  
  if (this.selectedRole) {
    console.log('Seçilen Rol Sınırları:', this.selectedRole.MinSalary, this.selectedRole.MaxSalary);
  }
}
onSubmit(): void {
  if (this.registerForm.valid) {
    const rawValue = this.registerForm.value;
    
    const registerData = {
      ...rawValue,
      salary: Number(rawValue.salary),           
      departmentId: Number(rawValue.departmentId), 
      roleId: Number(rawValue.roleId),           

    };

    this.authService.register(registerData).subscribe({
      next: (res) => {
        alert('Kayıt Başarılı!');
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {

        const errorMessage = err.error?.message || err.error || 'Yetki veya Veri Hatası!';
        alert('Hata: ' + errorMessage);
      }
    });
  } else {
    alert('Lütfen tüm alanları doğru şekilde doldurun!');
  }
}
}