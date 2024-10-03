using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TechStudy.RazorPages.Entities;
using TechStudy.RazorPages.Repositories;
using TechStudy.RazorPages.Services;

namespace TechStudy.RazorPages.Pages;

[Authorize]
public class GroupsModel : PageModel
{
    private readonly IGroupRepository _groupRepository;
    private readonly IApplicationService _applicationService;

    public GroupsModel(IGroupRepository groupRepository, IApplicationService applicationService)
    {
        _groupRepository = groupRepository;
        _applicationService = applicationService;
    }

    public IEnumerable<Group> Groups { get; set; }
    public Dictionary<int, int> GroupAppIds { get; set; }
    public async Task OnGetAsync()
    {
        string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Groups = await _groupRepository.GetAllAsync();
        GroupAppIds = (await _applicationService.GetAllAsync(userId))
            .ToDictionary(c => c.GroupId, c => c.Id);  
    }
}
