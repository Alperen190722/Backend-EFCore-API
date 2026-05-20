import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Role } from '../models/role/role';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5000/api/Roles';


  getRolesByDepartment(deptId: number): Observable<Role[]> {
    const token = typeof window !== 'undefined' ? sessionStorage.getItem('skylineToken') : null;
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<Role[]>(`${this.apiUrl}/getbydepartment?departmentId=${deptId}`, { headers });
  }


addRole(role: Role): Observable<string> {
  const token = typeof window !== 'undefined' ? sessionStorage.getItem('skylineToken') : null;
  const headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });

  return this.http.post(this.apiUrl, role, { 
    headers: headers, 
    responseType: 'text'
  });
}


updateRole(role: Role): Observable<string> {
  const token = sessionStorage.getItem('skylineToken');
  const headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });

  return this.http.put(`${this.apiUrl}/update`, role, { 
    headers: headers, 
    responseType: 'text'
  });
}

  deleteRole(id: number): Observable<any> {
  const token = typeof window !== 'undefined' ? sessionStorage.getItem('skylineToken') : null;
  const headers = new HttpHeaders({ 'Authorization': `Bearer ${token}` });
  
  return this.http.delete(`${this.apiUrl}/${id}`, { headers, responseType: 'text'});
}
}