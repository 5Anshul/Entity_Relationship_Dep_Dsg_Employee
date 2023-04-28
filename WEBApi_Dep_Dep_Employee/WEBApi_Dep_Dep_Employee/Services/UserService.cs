using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WEBApi_Dep_Dep_Employee.Identity;
using WEBApi_Dep_Dep_Employee.Models;
using WEBApi_Dep_Dep_Employee.Models.ViewModels;
using WEBApi_Dep_Dep_Employee.ServiceContract;

namespace WEBApi_Dep_Dep_Employee.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationSignInManager _applicationSignInManager;
        private readonly AppSettings _appSetting;
        public UserService(ApplicationUserManager applicationUserManager,
            ApplicationSignInManager applicationSignInManager, IOptions<AppSettings> appsetting)
        {
            _applicationSignInManager = applicationSignInManager;
            _applicationUserManager = applicationUserManager;
            _appSetting = appsetting.Value;
        }

        public async Task<ApplicationUser> Authenticate(UserVm userVm)
        {
            var result = await _applicationSignInManager.PasswordSignInAsync
        (userVm.UserName, userVm.UserPassword, false, false);
            if (result.Succeeded)
            {
                var applicationUser = await _applicationUserManager.
                  FindByNameAsync(userVm.UserName);
                applicationUser.PasswordHash = "";

                //JWT Token
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Admin))
                    applicationUser.Role = SD.Role_Admin;
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, SD.Role_Employee))
                    applicationUser.Role = SD.Role_Employee;

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name,applicationUser.Id),
                      new Claim(ClaimTypes.Email,applicationUser.Email),
                    new Claim(ClaimTypes.Role,applicationUser.Role)
                         }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)

                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                applicationUser.Token = tokenHandler.WriteToken(token);
                applicationUser.PasswordHash = "";

                return applicationUser;
            }
            else
            {
                return null;
            }
        }
    }
}
