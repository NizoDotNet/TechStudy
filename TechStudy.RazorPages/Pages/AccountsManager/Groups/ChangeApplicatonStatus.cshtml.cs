using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Entities;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager.Groups;

public class ChangeApplicatonStatus : PageModel
{
    private readonly IApplicationService _applicationService;

    public ChangeApplicatonStatus(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task<IActionResult> OnGetAcceptedAsync(int appId)
    {
        var res = await _applicationService.SetNewStatus(appId, new() { Id = (int)ApplicationStatusId.Accepted });
        if (res == 0)
            return BadRequest();
        return RedirectToPage("Applications");
    }
    public async Task<IActionResult> OnGetRejectedAsync(int appId)
    {
        var res = await _applicationService.SetNewStatus(appId, new() { Id = (int)ApplicationStatusId.Rejected });
        if (res == 0)
            return BadRequest();
        return RedirectToPage("Applications");
    }
}
