using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager.Claims;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUserService _userService;

    public IndexModel(IUserService userService)
    {
        _userService = userService;
    }

    public IEnumerable<IdentityUser> Users { get; set; }

    public IUserService UserService => _userService;

    public async Task<IActionResult> OnGetAsync()
    {
        Users = await _userService.GetAllAsync();

        return Page();
    }
}
