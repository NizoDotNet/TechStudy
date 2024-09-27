using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Entities;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Pages.AccountsManager.Groups
{
    public class IndexModel : PageModel
    {
        private readonly IGroupRepository _groupRepository;

        public IndexModel(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public IEnumerable<Group> Groups { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            Groups = await _groupRepository.GetAllAsync();
            return Page();
        }
    }
}
