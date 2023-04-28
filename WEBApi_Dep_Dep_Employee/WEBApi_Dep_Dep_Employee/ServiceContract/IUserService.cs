using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi_Dep_Dep_Employee.Identity;
using WEBApi_Dep_Dep_Employee.Models.ViewModels;

namespace WEBApi_Dep_Dep_Employee.ServiceContract
{
    public interface IUserService
    {
        Task<ApplicationUser> Authenticate(UserVm userVm);
    }
}
