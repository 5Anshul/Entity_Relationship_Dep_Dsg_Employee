export class Employee {
    employeeId:number;
    employeeName:string;
    employeeAddress:string;
    employeeEmail:string;
    employeeSalary:number;
    employeeAge:number;
    // departmentEmployees:any;
    departmentId:number[];
    departmentName:any;
    designationId:number;
    designationName:any;
    // department: any;
    // designation: any;
    constructor()
    {
        this.employeeId=0;
        this.employeeName="";
        this.employeeAddress="";
        this.employeeEmail="";
        this.employeeSalary=0;
        this.employeeAge=0;
        // this.departmentEmployees=null;
        this.designationId=0;
        this.designationName=null;
        this.departmentName=null;
        this.departmentId=[];
        // this.department=null;
        // this.designation=null;

    }
}




