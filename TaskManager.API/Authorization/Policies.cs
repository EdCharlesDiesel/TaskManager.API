using Microsoft.AspNetCore.Authorization;
using TaskManager.API.Authentication;

namespace TaskManager.API.Authorization
{
    public static class Policies
    {
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole(UserRoles.Admin)
                                                   .Build();
        }

        public static AuthorizationPolicy EmployeePolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole(UserRoles.Employee)
                                                   .Build();
        }
    }
}
