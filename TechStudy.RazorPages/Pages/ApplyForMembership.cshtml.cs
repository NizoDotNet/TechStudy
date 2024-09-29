using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TechStudy.RazorPages.Repositories;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages;

[Authorize]
public class ApplyForMembershipModel : PageModel
{
    private readonly IApplicationService _applicationService;

    public ApplyForMembershipModel(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task<IActionResult> OnGetAsync(int groupId)
    {
        string currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        await _applicationService.InsertAsync(new() { GroupId = groupId, TechStudyUserId = currentUserId });
        return RedirectToPage("Groups");
    }
}
