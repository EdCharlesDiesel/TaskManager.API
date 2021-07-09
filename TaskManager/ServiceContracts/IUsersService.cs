using TaskManager.Identity;
using TaskManager.ViewModels;
using System.Threading.Tasks;

namespace TaskManager.ServiceContracts
{
    public interface IUsersService
    {
        Task<ApplicationUser> Authenticate(LoginViewModel loginViewModel);
        Task<ApplicationUser> Register(SignUpViewModel signUpViewModel);
        Task<ApplicationUser> GetUserByEmail(string Email);
    }
}
