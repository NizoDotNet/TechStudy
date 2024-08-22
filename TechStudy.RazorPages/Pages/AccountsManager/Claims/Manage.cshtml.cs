using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager.Claims;

public class ManageModel : PageModel
{
    private readonly IUserService _userService;

    public ManageModel(IUserService userService)
    {
        _userService = userService;
    }

    public SelectList UserSelectList { get; set; }
    [BindProperty]
    public string SelectedUserId { get; set; }
    [BindProperty]
    public string ClaimType { get; set; }
    [BindProperty]
    public string ClaimValue { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        var users = await _userService.GetAllAsync();
        UserSelectList = new SelectList(users, nameof(IdentityUser.Id), nameof(IdentityUser.Email));

        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        var res = await _userService.AddClaimAsync(SelectedUserId, new(ClaimType, ClaimValue));
        if (res) return RedirectToPage("Index");
        return BadRequest();
    }
}
