using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApi_Dep_Dep_Employee.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; }
        public int Salary { get; set; }
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public string Role { get; set; }

    }
}
