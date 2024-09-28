using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Pages;

[Authorize]
public class ApplyForMembershipModel : PageModel
{
    private readonly IApplicationRepository _applicationRepository;

    public ApplyForMembershipModel(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<IActionResult> OnGetAsync(int groupId)
    {
        string currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _applicationRepository.InsertAsync(new() { GroupId = groupId, TechStudyUserId = currentUserId });
        return RedirectToPage("Groups");
    }
}
