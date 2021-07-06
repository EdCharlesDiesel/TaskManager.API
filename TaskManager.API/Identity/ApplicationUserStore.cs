using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskManager.API.Identity
{
    public class ApplicationUserStore : UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext applicationDbContext): base(applicationDbContext)
        {

        }
    }
}
