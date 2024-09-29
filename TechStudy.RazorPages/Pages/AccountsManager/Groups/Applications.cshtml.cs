using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Entities;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager.Groups;

public class ApplicationsModel : PageModel
{
    private readonly IApplicationService _applicationService;

    public ApplicationsModel(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public IEnumerable<ApplicationForMembership> ApplicationsForMembership { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        ApplicationsForMembership = await _applicationService.GetAllAsync();
        return Page();
    }
}
