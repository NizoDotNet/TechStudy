using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Entities;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Pages.AccountsManager.Groups;

public class CreateModel : PageModel
{
    private readonly IGroupRepository _groupRepository;

    public CreateModel(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }
    [BindProperty]
    public Group Group { get; set; }
    public async Task<IActionResult> OnPostAsync()
    {
        int res = await _groupRepository.InsertAsync(Group);
        if (res == 0) return BadRequest();
        return RedirectToPage("Index");
    }
}
