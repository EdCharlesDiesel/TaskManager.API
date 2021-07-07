using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Services;
using TaskManager.Identity;
using TaskManager.ViewModels;

namespace TaskManager.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IAntiforgery _antiforgery;
        private readonly ApplicationSignInManager _applicationSignInManager;

        public AccountController(IUsersService usersService, ApplicationSignInManager applicationSignManager, IAntiforgery antiforgery)
        {
            _usersService = usersService;
            _applicationSignInManager = applicationSignManager;
            _antiforgery = antiforgery;
        }

        [HttpPost]
        [Route("api/authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel loginViewModel)
        {
            var user = await _usersService.Authenticate(loginViewModel);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            HttpContext.User = await _applicationSignInManager.CreateUserPrincipalAsync(user);
            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);
            Response.Headers.Add("Access-Control-Expose-Headers", "XSRF-REQUEST-TOKEN");
            Response.Headers.Add("XSRF-REQUEST-TOKEN", tokens.RequestToken);

            return Ok(user);
        }
    }
}


