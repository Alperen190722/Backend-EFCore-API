import { HttpClient } from '@angular/common/http';
import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { Observable, tap, BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private platformId = inject(PLATFORM_ID);
  private apiUrl = 'http://localhost:5000/api/Auth';
  private router = inject(Router);
  private authStatus = new BehaviorSubject<boolean>(this.checkInitialStatus());
  authStatus$ = this.authStatus.asObservable();

  private checkInitialStatus(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return !!sessionStorage.getItem('skylineToken');
    }
    return false;
  }

  hasToken(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return !!sessionStorage.getItem('skylineToken');
    }
    return false;
  }

  constructor(private http: HttpClient) {}

  register(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Register`, data).pipe(
      tap((res: any) => {
        if (res && res.token) {
          sessionStorage.setItem('token', res.token);
        }
      }),
    );
  }

  getDepartments(): Observable<any> {
    const deptUrl = 'http://localhost:5000/api/Departments';
    return this.http.get<any>(deptUrl);
  }

  getRolesByDepartment(departmentId: number): Observable<any> {
    return this.http.get<any>(
      `http://localhost:5000/api/Roles/getbydepartment?departmentId=${departmentId}`,
    );
  }

  login(loginData: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/Login`, loginData).pipe(
      tap((res: any) => {
        const token = res?.Token || res?.token;
        if (token) {
          sessionStorage.setItem('skylineToken', token);

          try {
            const decoded: any = jwtDecode(token);

            const userId =
              decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] ||
              decoded['id'];
            const dept = decoded['Department'] || decoded['department'] || '';
            const empId = decoded['EmployeeId'] || decoded['id'] || '';
            const roleName =
              decoded['role'] ||
              decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ||
              '';

            const level = decoded['HierarchyLevel'] || decoded['hierarchyLevel'] || '0';

            if (userId) sessionStorage.setItem('userId', userId.toString());
            if (empId) sessionStorage.setItem('EmployeeId', empId.toString());

            sessionStorage.setItem('Department', dept);
            sessionStorage.setItem('Role', roleName);
            sessionStorage.setItem('HierarchyLevel', level.toString());

            const rawStatus =
              decoded['EmployeeStatus'] || decoded['Status'] || decoded['status'] || '1';
            sessionStorage.setItem('Status', rawStatus.toString());
          } catch (error) {
            console.error('Hata:', error);
          }

          this.authStatus.next(true);
        }
      }),
    );
  }

  forgotPassword(forgotPasswordDto: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/forgot-password`, forgotPasswordDto);
  }

  resetPassword(data: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/reset-password`, data, {
      responseType: 'text' as 'json',
    });
  }

  isAuthenticated(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return !!sessionStorage.getItem('skylineToken');
    }
    return false;
  }

  getRole(): string | null {
    if (!isPlatformBrowser(this.platformId)) return null;

    const token = sessionStorage.getItem('skylineToken');
    if (!token) return null;

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return (
        payload['role'] ||
        payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ||
        null
      );
    } catch (e) {
      return null;
    }
  }

  isAccounting(): boolean {
    return this.getRole() === 'Accounting';
  }

  loggedIn(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return !!sessionStorage.getItem('skylineToken');
    }
    return false;
  }

  logOut(isPassive: boolean = false) {
    if (isPassive) {
      const currentUserId = sessionStorage.getItem('userId');
    }

    sessionStorage.clear();
    this.authStatus.next(false);

    if (isPassive) {
      this.router.navigate(['/login'], { queryParams: { reason: 'passive' } });
      if (isPlatformBrowser(this.platformId)) {
      }
    } else {
      this.router.navigate(['/landing']);
    }
  }

  checkStatus(userId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/check-status/${userId}`);
  }
}
