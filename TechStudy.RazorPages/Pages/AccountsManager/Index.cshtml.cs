using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUserService _userService;

    public IndexModel(IUserService userService, ILogger<IndexModel> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    public IEnumerable<IdentityUser> Users { get; set; }

    public IUserService UserService => _userService;

    public async Task<IActionResult> OnGetAsync()
    {
        _logger.LogWarning("{User} with ID {ID} in Accounts Manager", 
            User.Identity!.Name, 
            User.FindFirstValue(ClaimTypes.NameIdentifier));

        Users = await _userService.GetAllAsync();
        
        return Page();
    }
}
