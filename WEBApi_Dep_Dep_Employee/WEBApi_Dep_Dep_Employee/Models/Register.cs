using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi_Dep_Dep_Employee.Identity;

namespace WEBApi_Dep_Dep_Employee.Models
{
    public class Register:ApplicationUser
    {
        public string EmployeeName { get; set; }
        public string RegisterEmail { get; set; }
        public string RegisterPassword { get; set; }
    }
}
