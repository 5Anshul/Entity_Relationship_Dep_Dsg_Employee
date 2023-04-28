import { Component } from '@angular/core';
import { Department } from '../department';
import { EmployeeService } from '../employee.service';
import { Employee } from '../employee';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.scss']
})
export class DepartmentComponent {
  departmentList: Department[] = [];

  constructor(private employeeService: EmployeeService) {}
  ngOnInit(): void {
    this.getAll();
  }
  getAll() {
    this.employeeService.getDepartment().subscribe(
      (response) => {
        this.departmentList = response;

        //console.log(this.designationList);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
