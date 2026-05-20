import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department } from '../models/department/department';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5000/api/Departments';


  getDepartments(): Observable<Department[]> {
    const token = typeof window !== 'undefined' ? sessionStorage.getItem('skylineToken') : null;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<Department[]>(this.apiUrl, { headers });
  }

addDepartment(dept: Department): Observable<string> {
  const token = typeof window !== 'undefined' ? sessionStorage.getItem('skylineToken') : null;
  const headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });

  return this.http.post(this.apiUrl, dept, { 
    headers: headers, 
    responseType: 'text' 
  });
}


  updateDepartment(dept: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/${dept.Id}`, dept);
  }

  deleteDepartment(id: number): Observable<any> {
  const token = typeof window !== 'undefined' ? sessionStorage.getItem('skylineToken') : null;
  const headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
  

  return this.http.delete(`${this.apiUrl}/${id}`, { headers, responseType: 'text' });
}
}