using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Entities;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Pages.AccountsManager.Groups;

public class IndexModel : PageModel
{
    private readonly IGroupRepository _groupRepository;
    private readonly ILogger<IndexModel> _logger;
    public IndexModel(IGroupRepository groupRepository, ILogger<IndexModel> logger)
    {
        _groupRepository = groupRepository;
        _logger = logger;
    }
    [FromRoute]
    public int PageNumber { get; set; }
    public IEnumerable<Group> Groups { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
        Groups = await _groupRepository.GetAllAsync(PageNumber);
        return Page();
    }
}
