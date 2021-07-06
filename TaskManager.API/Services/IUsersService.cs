using System.Threading.Tasks;
using TaskManager.API.Identity;
using TaskManager.API.viewModels;

namespace TaskManager.API.Services
{
    public interface IUsersService
    {
        Task<ApplicationUser> Authenticate(LoginViewModel loginViewModel);
    }
}
