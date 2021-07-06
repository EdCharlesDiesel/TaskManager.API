using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TaskManager.API.Identity
{
    public class ApplicationSignInManger : SignInManager<ApplicationUser>
    {
        public ApplicationSignInManger(ApplicationUserManager applicationUserManager, IHttpContextAccessor httpContextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory, IOptions<IdentityOptions> options,
            ILogger<ApplicationSignInManger> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation) :base(applicationUserManager,httpContextAccessor,userClaimsPrincipalFactory,
                options,logger,schemes,confirmation)
        {
            
        }
    }
}
