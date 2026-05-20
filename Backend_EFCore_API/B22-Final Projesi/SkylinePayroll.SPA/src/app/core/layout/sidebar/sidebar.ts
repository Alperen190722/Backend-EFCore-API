import { Component, OnInit, PLATFORM_ID, inject, ChangeDetectorRef } from '@angular/core'; 
import { RouterLink, RouterLinkActive, Router } from '@angular/router';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { AuthService } from '../../../services/auth.service';
import { Observable, tap } from 'rxjs'; 
import Swal from 'sweetalert2';
import { NotifService } from '../../../services/notif.service';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss',
})
export class Sidebar implements OnInit {

  userDepartment: string = '';
  authStatus$!: Observable<boolean>; 
  private statusCheckInterval: any;
  private _isLoggedIn: boolean = false;
  set isLoggedIn(value: boolean) {
  this._isLoggedIn = value;
  if (!value) {
    this.userDepartment = ''; 
  }
  this.cdr.detectChanges(); 
}

get isLoggedIn(): boolean {
  return this._isLoggedIn;
}
  private platformId = inject(PLATFORM_ID);
  private cdr = inject(ChangeDetectorRef);
  private authService = inject(AuthService);
  private router = inject(Router);
  private notif = inject(NotifService);

ngOnInit() {

    this.authStatus$ = this.authService.authStatus$.pipe(
      tap(status => {
        if (isPlatformBrowser(this.platformId)) {
          const token = sessionStorage.getItem('skylineToken');
          
          if (status && token) {
            this.userDepartment = sessionStorage.getItem('Department') || '';
            

            this.startHeartbeat();

          } else {
            this.userDepartment = '';

            this.stopHeartbeat();
          }
          this.cdr.detectChanges();
        }
      })
    );
  }

 private startHeartbeat() {
  if (this.statusCheckInterval) return;



  const myUserId = Number(sessionStorage.getItem('userId')); 
  
  if (!myUserId) return;

  this.statusCheckInterval = setInterval(() => {

    this.authService.checkStatus(myUserId).subscribe({
      next: (res) => {

        const backendStatus = res?.Status ?? res?.status;




        
        if (Number(backendStatus) === 3) { 

           this.stopHeartbeat();
           this.handleAccountClosure('Erişim İptali', 'Hesabınız pasife alınmıştır.');
        }
      },
      error: (err) => {

        console.error("Heartbeat hatası:", err);
      }
    });
  }, 15000); 
}
  private stopHeartbeat() {
    if (this.statusCheckInterval) {
      clearInterval(this.statusCheckInterval);
      this.statusCheckInterval = null;
    }
  }

  private handleAccountClosure(title: string, message: string) {
    sessionStorage.clear();
    
    Swal.fire({
      title: title,
      text: message,
      icon: 'error',
      confirmButtonText: 'Anlaşıldı',
      confirmButtonColor: '#1e40af',
      background: '#0a0a0a',
      color: '#ffffff'
    }).then(() => {
      this.router.navigate(['/landing']);
      if (isPlatformBrowser(this.platformId)) {
        window.location.reload();
      }
    });
  }
}