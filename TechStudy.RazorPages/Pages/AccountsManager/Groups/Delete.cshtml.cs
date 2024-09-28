using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Pages.AccountsManager.Groups;

public class DeleteModel : PageModel
{
    private readonly IGroupRepository _groupRepository;

    public DeleteModel(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var res = await _groupRepository.DeleteAsync(id);
        if (res == 0) return BadRequest();
        return RedirectToPage("Index");
    }
}
