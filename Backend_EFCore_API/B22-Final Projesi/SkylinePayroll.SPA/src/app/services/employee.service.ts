import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Employee } from '../models/employee/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5000/api/employees';
  private apiUrl2 = 'http://localhost:5000/api';

  getEmployees(): Observable<any> {
    const token = typeof window !== 'undefined' ? sessionStorage.getItem('skylineToken') : null;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<any>(this.apiUrl, { headers });
  }

  updateEmployee(employeeDto: any): Observable<string> {
  return this.http.put(`${this.apiUrl}/update`, employeeDto, { 
    responseType: 'text'
  });
}


updateStatus(id: number, status: number): Observable<any> {

  return this.http.put(`${this.apiUrl}/update-status?id=${id}&status=${status}`, {},{responseType: 'text'});
}



getDepartments(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl2}/Departments`);
}

getRoles(departmentId?: number): Observable<any[]> {
  if (departmentId) {
    return this.http.get<any[]>(`${this.apiUrl2}/Roles/getbydepartment?departmentId=${departmentId}`);
  }


  return this.http.get<any[]>(`${this.apiUrl2}/Roles/getbydepartment?departmentId=0`);
}

getFilteredManagementList(): Observable<Employee[]> {
    return this.http.get<Employee[]>(`${this.apiUrl}/get-filtered-management-list`);
  }
}
