import { Designation } from './designation';
import { Department } from './department';
import { Employee } from './employee';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Register } from './register';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  constructor(private httpClient: HttpClient) {}
  getAllEmployees(): Observable<any> {
    return this.httpClient.get<any>(
      'https://localhost:44362/api/employee'
    );
  }
  saveEmployee(newEmployee: Employee): Observable<Employee> {
    return this.httpClient.post<Employee>(
      'https://localhost:44362/api/employee',
      newEmployee
    );
  }
  updateEmployee(editEmployee: Employee): Observable<Employee> {
    return this.httpClient.put<Employee>(
      'https://localhost:44362/api/employee',
      editEmployee
    );
  }
  deleteEmployee(id: number): Observable<any> {
    return this.httpClient.delete<any>(
      'https://localhost:44362/api/employee/' + id
    );
  }
  getDepartment(): Observable<any> {
    return this.httpClient.get<any>('https://localhost:44362/api/department');
  }
  getDesignation(): Observable<any> {
    return this.httpClient.get<any>('https://localhost:44362/api/designation  ');
  }
  registerEmployee(newRegister:Register):Observable<Register>{
    return this.httpClient.post<Register>('https://localhost:44362/api/user/Register',newRegister);
  }
}
