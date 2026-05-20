import { Component, OnInit, inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, DatePipe, isPlatformBrowser } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { NotifService } from '../../services/notif.service';
import { AuthService } from '../../services/auth.service';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink, AsyncPipe, DatePipe],
  templateUrl: './navbar.html',
  styleUrl: './navbar.scss',
})
export class Navbar implements OnInit {
  private router = inject(Router);
  public notifService = inject(NotifService);
  public authService = inject(AuthService);
  public cdr = inject(ChangeDetectorRef);
  private platformId = inject(PLATFORM_ID);

  isLoggedIn = false;
  notifications$ = this.notifService.notifications$;

  ngOnInit() {
    this.authService.authStatus$.subscribe((status: boolean) => {
      this.isLoggedIn = status;

      if (status) {
        this.notifService.refreshNotifications();
      } else {
        this.isLoggedIn = false;
      }

      this.cdr.detectChanges();
    });

    if (isPlatformBrowser(this.platformId)) {
      this.isLoggedIn = this.authService.loggedIn();
    }
  }

  checkAuth() {
    if (isPlatformBrowser(this.platformId)) {
      this.isLoggedIn = !!sessionStorage.getItem('skylineToken');
    }
  }

  getUnreadCount(notifications: any[] | null): number {
    if (!notifications) return 0;
    return notifications.filter((n) => !n.IsRead).length;
  }

  markAsRead(id: number, event: Event) {
    event.stopPropagation();
    if (!id) return;
    this.notifService.markAsRead(id).subscribe(() => {
      this.notifService.refreshNotifications();
    });
  }

  clearNotif(id: number, event: Event) {
    event.stopPropagation();
    if (!id) return;
    this.notifService.clearNotification(id).subscribe(() => {
      this.notifService.refreshNotifications();
    });
  }

  logout() {
    if (isPlatformBrowser(this.platformId)) {
      sessionStorage.clear();
      this.authService.logOut();
    }
  }
}
