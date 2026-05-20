import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PayrollService {

  private apiUrl = 'http://localhost:5000/api/Payrolls'; 
  private http = inject(HttpClient);


  getDashboardSummary(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/getsummary`);
  }


  runMonthlyPayroll(): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/run-monthly`, {});
  }


  getEmployeePayrollHistory(employeeId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/history/${employeeId}`);
  }
}
