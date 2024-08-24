using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TechStudy.RazorPages.AuthorizationRequirements
{
    public class AdminRoleRequirementHandler : IAuthorizationHandler
    {
        
        public async Task HandleAsync(AuthorizationHandlerContext context)
        {

            string? role = context.User.FindFirstValue("Role");
            if (role == null)
            {
                return;
            }
            foreach (var req in context.Requirements)
            {
                if(req is AdminRoleRequirement)
                {
                    if (role == "Admin")
                    {
                        context.Succeed(req);
                    }
                    break;
                }
            }
            

        }
    }
}
