using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TaskManager.API.Identity;

namespace TaskManager.Identity
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbContext>
    {
        public ApplicationRoleStore(ApplicationDbContext applicationDbContext, IdentityErrorDescriber identityErrorDescriber) : base(applicationDbContext, identityErrorDescriber)
        {

        }
    }
}


