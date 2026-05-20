import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, BehaviorSubject, tap } from 'rxjs';
import Swal from 'sweetalert2';

@Injectable({ providedIn: 'root' })
export class NotifService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5000/api/Notification/';


  private notificationsSubject = new BehaviorSubject<any[]>([]);
  public notifications$ = this.notificationsSubject.asObservable();

  getMyNotifications(): Observable<any> {
    const token = sessionStorage.getItem('skylineToken');
    if (!token) return of({ data: [], success: false });

    return this.http.get<any>(this.apiUrl + 'GetMyNotifications').pipe(
      tap(res => {

        const data = res?.data || res?.Data || res || [];
        this.notificationsSubject.next(data);
      })
    );
  }


  clearNotification(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}clear-notification/${id}`).pipe(
      tap(() => this.refreshNotifications())
    );
  }


  markAsRead(id: number): Observable<any> {
    return this.http.post(`${this.apiUrl}mark-as-read/${id}`, {}).pipe(
      tap(() => this.refreshNotifications())
    );
  }



  clearActionNotification(actionId: number, type: string): Observable<any> {
    const dto = {
      TargetActionId: actionId,
      NotificationType: type
    };

    return this.http.post(this.apiUrl + 'clear-process-notifications', dto).pipe(
      tap(() => this.refreshNotifications())
    );
  }

  sendNotification(message: string, type: string, actionId: number, targetUserId?: number, targetDeptId?: number): Observable<any> {
    const dto = {
      Message: message,
      NotificationType: type,
      TargetActionId: actionId,
      TargetUserId: targetUserId || null,
      TargetDepartmentId: targetDeptId || null
    };
    return this.http.post(this.apiUrl + 'send-notification', dto).pipe(
      tap(() => this.refreshNotifications())
    );
  }

  refreshNotifications() {
    this.getMyNotifications().subscribe();
  }


  success(message: string, title: string = 'İşlem Başarılı') {
    Swal.fire({
      title: `<span style="color: #27ae60">${title}</span>`,
      text: message,
      icon: 'success',
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: 3000,
      timerProgressBar: true,
      background: '#fff',
      iconColor: '#27ae60'
    });
  }

  error(message: string, title: string = 'Sistem Hatası') {
    Swal.fire({
      title: title,
      text: message,
      icon: 'error',
      confirmButtonText: 'Anlaşıldı',
      confirmButtonColor: '#e74c3c'
    });
  }
}