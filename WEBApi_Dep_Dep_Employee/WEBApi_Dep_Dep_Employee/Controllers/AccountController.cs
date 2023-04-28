using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApi_Dep_Dep_Employee.Models.ViewModels;
using WEBApi_Dep_Dep_Employee.ServiceContract;

namespace WEBApi_Dep_Dep_Employee.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]UserVm userVm)
        {
            var user = await _userService.Authenticate(userVm);
            if (user == null) return BadRequest(new { message = "Wrong User/Password" });
            return Ok(user);
        }
    }
}
