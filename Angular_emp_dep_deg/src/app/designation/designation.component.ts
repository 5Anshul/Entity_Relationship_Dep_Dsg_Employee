import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../employee.service';
import { Employee } from '../employee';

@Component({
  selector: 'app-designation',
  templateUrl: './designation.component.html',
  styleUrls: ['./designation.component.scss'],
})
export class DesignationComponent implements OnInit {
  designationList: Employee[] = [];

  constructor(private employeeService: EmployeeService) {}
  ngOnInit(): void {
    this.getAll();
  }
  getAll() {
    this.employeeService.getDesignation().subscribe(
      (response) => {
        this.designationList = response;

        //console.log(this.designationList);
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
