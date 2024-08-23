using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
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

        public IUserService UserService => _userService;
        public IEnumerable<IdentityUser> Users { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _userService.GetAllAsync();

            return Page();

        }

        
    }
}
