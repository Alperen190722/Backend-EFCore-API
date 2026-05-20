import { Component, inject } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-landing',
  standalone:true,
  imports: [RouterModule, CommonModule],
  templateUrl: './landing.html',
  styleUrl: './landing.scss',
})
export class Landing {
  public authService = inject(AuthService); 
  private router = inject(Router);

  onLogout() {
    this.authService.logOut();
    this.router.navigate(['/landing']);
  }
}
