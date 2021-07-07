﻿using System.Threading.Tasks;
using TaskManager.Identity;
using TaskManager.ViewModels;

namespace TaskManager.API.Services
{
    public interface IUsersService
    {
        Task<ApplicationUser> Authenticate(LoginViewModel loginViewModel);
    }
}
