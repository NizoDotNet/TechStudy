using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TechStudy.RazorPages.Repositories;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages;

[Authorize]
public class DeleteApplicationModel : PageModel
{
    private readonly IApplicationRepository _applicationRepository;

    public DeleteApplicationModel(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<IActionResult> OnGet(int appId)
    {
        await _applicationRepository.DeleteAsync(appId, User.FindFirstValue(ClaimTypes.NameIdentifier));

        return RedirectToPage("Groups", new { paganumber = 1 });
    }
}
