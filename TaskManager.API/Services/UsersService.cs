
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManager.Identity;
using TaskManager.ViewModels;
using TaskManager.API.Services;

namespace TaskManager.Services
{
    public class UsersService : IUsersService
    {
        private readonly AppSettings _appSettings;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationSignInManager _applicationSignInManager;

        public UsersService(ApplicationUserManager applicationUserManager, ApplicationSignInManager applicationSignInManager, IOptions<AppSettings> appSettings)
        {
            _applicationUserManager = applicationUserManager;
            _applicationSignInManager = applicationSignInManager;
            _appSettings = appSettings.Value;
        }

        public async Task<ApplicationUser> Authenticate(LoginViewModel loginViewModel)
        {
            var result = await _applicationSignInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                var applicationUser = await _applicationUserManager.FindByNameAsync(loginViewModel.Username);
                applicationUser.PasswordHash = null;
                if (await _applicationUserManager.IsInRoleAsync(applicationUser, "Admin")) applicationUser.Role = "Admin";
                else if (await _applicationUserManager.IsInRoleAsync(applicationUser, "Employee")) applicationUser.Role = "Employee";

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = System.Text.Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, applicationUser.Id),
                        new Claim(ClaimTypes.Email, applicationUser.Email),
                        new Claim(ClaimTypes.Role, applicationUser.Role)
                    }),
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key), Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                applicationUser.Token = tokenHandler.WriteToken(token);

                return applicationUser;
            }
            else
            {
                return null;
            }
        }
 
    }
}


