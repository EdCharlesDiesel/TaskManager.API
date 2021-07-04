using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskManager.API.Identity
{
    public class ApplicationRoleStore:RoleStore<ApplicationRoles,ApplicationDbContext>
    {
        public ApplicationRoleStore(ApplicationDbContext applicationDbContext,IdentityErrorDescriber identityErrorDescriber): base(applicationDbContext,identityErrorDescriber)
        {

        }
    }
}
