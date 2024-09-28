using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechStudy.RazorPages.Entities;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Pages
{
    public class GroupsModel : PageModel
    {
        private readonly IGroupRepository _groupRepository;

        public GroupsModel(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public IEnumerable<Group> Groups { get; set; }
        public async Task OnGetAsync()
        {
            Groups = await _groupRepository.GetAllAsync();
        }
    }
}
