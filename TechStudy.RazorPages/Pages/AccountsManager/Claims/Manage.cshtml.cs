using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager.Claims;

public class ManageModel : PageModel
{
    private readonly IUserService _userService;
    private readonly ILogger<ManageModel> _logger;
    public ManageModel(IUserService userService, ILogger<ManageModel> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public IdentityUser IdentityUser { get; set; }
    [BindProperty]
    public string UserId { get; set; }
    [BindProperty]
    public string ClaimType { get; set; }
    [BindProperty]
    public string ClaimValue { get; set; }
    public async Task<IActionResult> OnGetAsync(string userId)
    {
        IdentityUser = await _userService.GetAsync(userId);
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        Claim claim = new(ClaimType, ClaimValue);
        Claim oldClaim = await _userService.GetClaimAsync(UserId, ClaimType);
        bool res = false;
        if (oldClaim is not null)
        {
            res = await _userService.ReplaceClaimAsync(UserId, oldClaim, claim);

        }
        else
        {
            res = await _userService.AddClaimAsync(UserId, new(ClaimType, ClaimValue));
        }
        if (res)
        {
            _logger.LogWarning("{Claim} was added to {UserId} by {AdminEmail}", 
                claim, UserId, User.FindFirstValue(ClaimTypes.Email));
            return RedirectToPage("Index", new { userId = UserId });
        }
        return BadRequest();
    }
}
