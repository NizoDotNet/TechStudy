using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Pages.Account;

public class IndexModel : PageModel
{
    private readonly IUserRepository _userRepository;

    public IndexModel(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool IsCurrentUser { get; set; } = false;
    public IdentityUser Account { get; set; }
    public async Task<IActionResult> OnGetAsync(string id)
    {
        var user = await _userRepository.GetAsync(id);
        if(user is null) 
            { return NotFound(); }
        Account = user;
        if (user.Id == HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value)
        {
            IsCurrentUser = true;
        }
        return Page();
    }
}
