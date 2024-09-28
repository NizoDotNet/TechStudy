using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager.Accounts;

public class DeleteModel : PageModel
{
    private readonly IUserService _userService;
    private readonly ILogger<DeleteModel> _logger;
    public DeleteModel(IUserService userService)
    {
        _userService = userService;
    }
    public async Task<IActionResult> OnGetAsync(string id)
    {
        var user = await _userService.GetAsync(id);
        if (user is null)
        {
            return NotFound();
        }
        var res = await _userService.Delete(id);
        if(res)
        {
            _logger.LogWarning("{Email} was deleted as user by {AdminEmail}", 
                user.Email, User.FindFirstValue(ClaimTypes.Email));
            return RedirectToPage("Index");
        }
        return BadRequest();
    }

}
