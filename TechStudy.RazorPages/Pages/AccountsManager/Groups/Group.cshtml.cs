using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Entities;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Pages.AccountsManager.Groups
{
    public class GroupModel : PageModel
    {
        private readonly IGroupRepository _groupRepository;

        public GroupModel(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public Group? Group { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Group = await _groupRepository.GetByIdAsync(id);
            if(Group == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
