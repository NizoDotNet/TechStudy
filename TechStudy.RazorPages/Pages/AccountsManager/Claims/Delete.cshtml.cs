using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager.Claims
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;

        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string type, string value)
        {
            var res = await _userService.RemoveClaimAsync(userId, new(type, value));
            if (res)
            {
                return RedirectToPage("Index", new { userId = userId});  
            }
            return BadRequest();

        }
    }
}
