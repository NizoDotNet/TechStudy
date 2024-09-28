using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages.AccountsManager.Groups;

public class RemoveAccountModel : PageModel
{
    private IGroupService _groupService;

    public RemoveAccountModel(IGroupService groupService)
    {
        _groupService = groupService;
    }

    public async Task<IActionResult> OnGetAsync(int groupId, string userId)
    {
        var res = await _groupService.RemoveAccount(groupId, userId);
        if (res == 0) return BadRequest();
        return RedirectToPage("/Index");
    }
}
