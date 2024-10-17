using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TechStudy.RazorPages.AuthorizationRequirements;

public class AdminRoleRequirement : IAuthorizationRequirement, IAuthorizationHandler
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
            if (req is AdminRoleRequirement)
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
