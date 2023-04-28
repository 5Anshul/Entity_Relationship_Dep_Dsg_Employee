import { LoginService } from './login.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
[x: string]: any;
  title = 'WebApifor_emp_dep_deg';
  constructor(public loginService:LoginService){}
  logOutClick()
  {
    this.loginService.LogOut();
  }
  
}
