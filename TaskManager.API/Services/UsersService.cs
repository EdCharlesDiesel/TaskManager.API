using System;
using System.Threading.Tasks;
using TaskManager.API.Identity;
using TaskManager.API.viewModels;

namespace TaskManager.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly ApplicationSignInManger _applicationSignInManger;

        public UsersService(ApplicationUserManager applicationUserManager, ApplicationSignInManger applicationSignInManger)
        {
            this._applicationSignInManger = applicationSignInManger ?? throw new ArgumentNullException(nameof(applicationSignInManger));
            this._applicationUserManager= applicationUserManager?? throw new ArgumentNullException(nameof(applicationUserManager));
        }
        public async Task<ApplicationUser> Authenticate(LoginViewModel loginViewModel)
        {
            var result = await _applicationSignInManger.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                var applicationUser = await _applicationUserManager.FindByNameAsync(loginViewModel.UserName);
                applicationUser.PasswordHash = null;
                return applicationUser;
            }
            else
                return null;

        }
    }
}
