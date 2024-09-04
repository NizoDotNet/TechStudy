using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.Security.Claims;
using TechStudy.RazorPages.Data;
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

        public TechStudyUser? TechStudyUser { get; set; }
        public IEnumerable<Claim> UserClaims { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId)
        {
            TechStudyUser = await _userService.GetAsync(userId);
            if(TechStudyUser is null)
            {
                return NotFound();
            }
            UserClaims = await _userService.GetClaimsAsync(TechStudyUser);

            return Page();

        }

        
    }
}
