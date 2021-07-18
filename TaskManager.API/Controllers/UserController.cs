using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TaskManager.API.Authentication;
using TaskManager.API.Helpers;
using TaskManager.API.Models;
using TaskManager.API.Services;

namespace TaskManager.API.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;        

        public UserController(IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;            
        }

        /// <summary>
        /// Get the count of item in the shopping cart
        /// </summary>
        /// <param name="userId"></param>        
        [HttpGet("{userId}")]
        public string Get(string userId)
        {            
            var user = _userService.GetById(userId);

            return user.UserId;
        }

        /// <summary>
        /// Check the availability of the username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("validateUserName/{userName}")]
        public bool ValidateUserName(string userName)
        {
            return _userService.CheckUserAvailabity(userName);
        }

        ///// <summary>
        ///// Register a new user
        ///// </summary>
        ///// <param name="userData"></param>
        //[HttpPost]
        //public void Post([FromBody] UserMaster userData,string password)
        //{
        //    _userService.RegisterUser(userData,password);
        //}


        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<UserMaster>(model);

            try
            {
                // create user
                _userService.RegisterUser(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
