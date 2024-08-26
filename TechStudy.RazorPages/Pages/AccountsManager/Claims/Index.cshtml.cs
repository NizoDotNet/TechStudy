using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Security.Claims;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager.Claims
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IdentityUser? IdentityUser { get; set; }
        public IEnumerable<Claim> UserClaims { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            IdentityUser = await _userService.GetAsync(userId);
            if(IdentityUser is null)
            {
                return NotFound();
            }
            UserClaims = await _userService.GetClaimsAsync(IdentityUser);

            return Page();

        }

        
    }
}
