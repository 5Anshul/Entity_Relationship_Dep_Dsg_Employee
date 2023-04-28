import { Designation } from './../designation';
import { EmployeeService } from './../employee.service';
import { Component, OnInit } from '@angular/core';
import { Employee } from '../employee';
import { Department } from '../department';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.scss'],
})
export class EmployeeComponent implements OnInit {
  employeeList: Employee[] = [];
  departmentList: Department[] = [];
  designationList: Designation[] = [];
  newEmployee: Employee = new Employee();
  editEmployee: Employee = new Employee();
  dropdownSettings: IDropdownSettings = {};

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.getAll();
    this.getDep();
    this.getDsg();

    // this.departmentList = [];
    // this.dropdownSettings = {
    //   idField: 'departmentId',
    //   textField: 'departmentName',
    //   allowSearchFilter: true,
    // };
  }
  confirmBox() {
    Swal.fire({
      title: 'Are you sure want to remove?',
      text: 'You will not be able to recover this file!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it',
    }).then((result) => {
      if (result.value) {
        Swal.fire(
          'Deleted!',
          'Your imaginary file has been deleted.',
          'success'
        );
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire('Cancelled', 'Your imaginary file is safe :)', 'error');
      }
    });
  }

  getAll() {
    this.employeeService.getAllEmployees().subscribe(
      (response) => {
        this.employeeList = response;

        //console.log(this.employeeList);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getDep() {
    this.employeeService.getDepartment().subscribe(
      (response) => {
        this.departmentList = response;
        //console.log(this.departmentList);
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getDsg() {
    this.employeeService.getDesignation().subscribe(
      (response) => {
        this.designationList = response;
        // console.log(this.designationList);
      },
      (error) => {
        console.log(error);
      }
    );
  }
  SaveClick() {
    //debugger;
    // alert(this.newEmployee.employeeName);
    // alert(this.newEmployee.employeeAddress);
    // alert(this.newEmployee.employeeEmail);
    // alert(this.newEmployee.employeeSalary);
    // alert(this.newEmployee.departmentId);
    // alert(this.newEmployee.designationId);
    
 

    this.employeeService.saveEmployee(this.newEmployee).subscribe(
      (response) => {
        this.getAll();
        Swal.fire('Thank you...', 'You submitted succesfully!', 'success');
      },
      (error) => {
        console.log(error);
        Swal.fire('Data Not Added !!!');
      }
    );
  }
  // SaveClick() {
  //   this.employeeService.saveEmployee(this.newEmployee).subscribe(
  //     (response) => {
  //       this.getAll();
  //       const getDeptId = this.newEmployee.departmentId.map((x) => x.departmentId);
  //       this.newEmployee.departmentId = getDeptId;
  //       Swal.fire('Thank you...', 'You submitted successfully!', 'success');
  //     },
  //     (error) => {
  //       console.log(error);
  //       Swal.fire('Data Not Added !!!');
  //     }
  //   );
  // }


  EditClick(employee: Employee) {
    this.editEmployee = employee;
  }
  UpdateClick() {
    // alert(this.editEmployee.employeeName);
    // alert(this.editEmployee.employeeAddress);
    // alert(this.editEmployee.employeeEmail);
    // alert(this.editEmployee.employeeSalary);
    // alert(this.editEmployee.employeeAge);
    // alert(this.editEmployee.departmentId);
    // alert(this.editEmployee.designationId);
    // debugger;
    this.employeeService.updateEmployee(this.editEmployee).subscribe(
      (response) => {
        this.getAll();
        Swal.fire('Thank you...', 'Data Updated succesfully!', 'success');
      },
      (error) => {
        console.log(error);
        Swal.fire('Data Not Updated !!!');
      }
    );
  }

  DeleteClick(id: number) {
    this.employeeService.deleteEmployee(id).subscribe(
      (result) => {
        this.getAll();
        Swal.fire('Data Delete Successfully');
      },

      (error) => {
        console.log(error);
        Swal.fire('Unable to Delete Data');
      }
    );
  }
}