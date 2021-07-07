using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TaskManager.API.Identity;

namespace TaskManager.Identity
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}


