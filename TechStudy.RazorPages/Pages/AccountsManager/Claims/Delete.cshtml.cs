using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager.Claims
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ILogger<DeleteModel> _logger;
        public DeleteModel(IUserService userService, ILogger<DeleteModel> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string type, string value)
        {
            var res = await _userService.RemoveClaimAsync(userId, new(type, value));
            if (res)
            {
                _logger.LogWarning("User's claim with {type} {value} " +
                    "was deleted by {AdminEmail}. User's {userId}", 
                    type, value, User.FindFirstValue(ClaimTypes.Email),userId);
                return RedirectToPage("Index", new { userId = userId});  
            }
            return BadRequest();

        }
    }
}
