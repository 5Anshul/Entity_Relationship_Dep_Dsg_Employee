import { Component } from '@angular/core';
import { EmployeeService } from '../employee.service';
import Swal from 'sweetalert2';
import { Employee } from '../employee';
import { Register } from '../register';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  newRegister: Register = new Register();
  employeeList: Employee[] = [];
  constructor(private employeeService: EmployeeService) {}

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
  registerClick() {
    debugger;
    alert(this.newRegister.employeeName);
    alert(this.newRegister.registerEmail);
    alert(this.newRegister.registerPassword);
    this.employeeService.registerEmployee(this.newRegister).subscribe(
      (response) => {        
        this.newRegister.employeeName = '';
        this.newRegister.registerEmail = '';
        this.newRegister.registerPassword = '';
        Swal.fire('User succesfully Registered !!!', 'success');
      },
      (error) => {
        console.log(error);
        Swal.fire('User not Registered !!!');
      }
    );
  }
}
