import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Result } from '../models/result/result';
import { Observable } from 'rxjs';
import { Termination } from '../models/termination/termination';
import { Resignation } from '../models/resignation/resignation';

@Injectable({ providedIn: 'root' })
export class TerminationService {
  private apiUrl = 'http://localhost:5000/api/terminations';

  constructor(private http: HttpClient) {}


  initiate(employeeId: number, reason: string): Observable<Result> {
    return this.http.post<Result>(
      `${this.apiUrl}/initiate-termination/${employeeId}`,
      JSON.stringify(reason),
      { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) }
    );
  }



  approveHR(employeeId: number): Observable<Result> {
    return this.http.post<Result>(`${this.apiUrl}/approve-hr/${employeeId}`, {});
  }



  employeeApprove(employeeId: number): Observable<Result> {
    return this.http.post<Result>(`${this.apiUrl}/employee-approve/${employeeId}`, {});
  }



  getDetailForEmployee(employeeId: number): Observable<Result> {
    return this.http.get<Result>(`${this.apiUrl}/get-detail-for-employee/${employeeId}`);
  }


  getPendingAccountingPayments(): Observable<Result> {
    return this.http.get<Result>(`${this.apiUrl}/get-pending-accounting`);
  }


  finalize(employeeId: number, type: number): Observable<Result> {
    return this.http.post<Result>(`${this.apiUrl}/finalize-termination/${employeeId}`, type, {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }




  submitResignation(dto: Resignation): Observable<any> {

    return this.http.post(`${this.apiUrl}/submit-resignation`, dto);
  }


  approveResignation(employeeId: number): Observable<any> {

    return this.http.post(`${this.apiUrl}/approve-resignation/${employeeId}`, {});
  }


  getAllHistory(): Observable<Result> {
    return this.http.get<Result>(`${this.apiUrl}/getall`);
  }
}