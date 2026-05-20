import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { FormsModule } from '@angular/forms'; 
import { AuthService } from '../../services/auth.service'; 
import { RouterModule, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { NotifService } from '../../services/notif.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './login.html',
  styleUrls: ['./login.scss'],
})
export class Login {

  loginData = {
    email: '',
    password: '',
  };

  constructor(
    private authService: AuthService,
    private router: Router,
    private notif: NotifService
  ) {}

onLogin() {
  this.authService.login(this.loginData).subscribe({
    next: (res) => {

      if (res.Status === '3' || res.status === '3') {
        Swal.fire({
          title: 'Giriş Başarısız',
          text: res.Message || "Hesabınız pasif durumdadır.", 
          icon: 'error'
        });
        this.authService.logOut();
      } else {
        this.router.navigate(['/dashboard']);
      }
    },
error: (err) => {
  const msg = err.error?.Message || "Giriş yapılamadı.";
  Swal.fire({
    title: 'Sistem Uyarısı',
    text: msg,
    icon: 'error',
    background: '#0a0a0a',
    color: '#fff'
  });
}
  });
}
}