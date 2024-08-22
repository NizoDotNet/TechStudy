using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager
{
    public class DeleteModel : PageModel
    {
        private readonly IUserService _userService;
        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            var res = await _userService.Delete(id);
            if(res)
            {
                return RedirectToPage("Index");
            }
            return BadRequest();
        }

    }
}
