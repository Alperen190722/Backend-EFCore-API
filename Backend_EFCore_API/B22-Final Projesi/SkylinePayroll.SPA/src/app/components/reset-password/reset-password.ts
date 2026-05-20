import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-reset-password',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './reset-password.html',
  styleUrl: './reset-password.scss',
})
export class ResetPassword implements OnInit {
  private route = inject(ActivatedRoute);
  private authService = inject(AuthService);
  private router = inject(Router);

  token: string = '';
  email: string = '';
  newPassword: string = '';
  confirmPassword: string = '';

  ngOnInit() {
    const params = this.route.snapshot.queryParamMap;
    


    this.token = params.get('Token') || params.get('token') || ''; 
    this.email = params.get('Email') || params.get('email') || ''; 

    if (!this.token || !this.email) {
      alert('Geçersiz veya eksik bağlantı! Tekrar dene.');
      this.router.navigate(['/forgot-password']);
    }
  }

  onResetPassword() {
    if (this.newPassword !== this.confirmPassword) {
      alert('Şifreler birbiriyle eşleşmiyor!');
      return;
    }


    const resetData = {
      Email: this.email,
      Token: this.token,
      NewPassword: this.newPassword 
    };

    this.authService.resetPassword(resetData).subscribe({
      next: (res) => {
        alert('Şifreniz başarıyla güncellendi! Giriş yapabilirsiniz.');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        alert(err.error?.message || 'Bir hata oluştu. Token geçersiz veya süresi dolmuş olabilir.');
      }
    });
  }
}
