import { Component, inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser, CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [FormsModule, CommonModule, RouterModule],
  templateUrl: './forgot-password.html',
  styleUrls: ['./forgot-password.scss'],
})
export class ForgotPassword {
  email: string = '';
  isLoading: boolean = false;

  private authService = inject(AuthService);
  private router = inject(Router);
  private platformId = inject(PLATFORM_ID);

  onSendResetLink() {
    if (!this.email) return;
    this.isLoading = true;
    const forgotData = { Email: this.email };
    this.authService.forgotPassword(forgotData).subscribe({
      next: (res: any) => {
        this.isLoading = false;

        const capturedToken = res.token || res.Token;

        if (isPlatformBrowser(this.platformId)) {
          alert('Şifre sıfırlama anahtarınız oluşturuldu! Yönlendiriliyorsunuz.');
        }

        this.router.navigate(['/reset-password'], {
          queryParams: {
            Token: capturedToken, 
            Email: this.email,
          },
        });
      },
      error: (err) => {
        this.isLoading = false;
        alert(err.error?.message || 'Bir hata oluştu.');
      },
    });
  }
}
