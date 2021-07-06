using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManager.API.Services;
using TaskManager.API.viewModels;
using System;

namespace TaskManager.API.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUsersService _usersService;
        public AccountController( IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [HttpPost]
        [Route("/authenticate")]
        public async  Task<IActionResult> Authenticate([FromBody] LoginViewModel loginViewModel)
        {
            var user = await _usersService.Authenticate(loginViewModel);
            if (user ==null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            return Ok(user);

        }
    }
}
