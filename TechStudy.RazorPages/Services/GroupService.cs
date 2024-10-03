using Microsoft.EntityFrameworkCore;
using TechStudy.RazorPages.Data;
using TechStudy.RazorPages.Entities;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly ApplicationDbContext _db;
    public GroupService(IGroupRepository groupRepository, ApplicationDbContext db)
    {
        _groupRepository = groupRepository;
        _db = db;
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _groupRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Group>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        return await _groupRepository.GetAllAsync(pageNumber, pageSize);
    }

    public async Task<Group> GetByIdAsync(int id)
    {
        return await _groupRepository.GetByIdAsync(id);
    }

    public async Task<int> InsertAsync(Group group)
    {
        return await _groupRepository.InsertAsync(group);
    }

    public async Task<int> RemoveAccount(int groupId, string userId)
    {
        var group = await _db.Groups
            .Include(c => c.TechStudyUsers)
            .FirstOrDefaultAsync(c => c.Id == groupId);
        if (group is null) return 0;
        var user = await _db.Users.FirstOrDefaultAsync(c => c.Id == userId);
        if (user is null) return 0;

        group.TechStudyUsers.Remove(user);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(int Id, Group updated)
    {
        return await _groupRepository.UpdateAsync(Id, updated);
    }
}
