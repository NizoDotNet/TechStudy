using Microsoft.EntityFrameworkCore;
using TechStudy.RazorPages.Data;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<GroupRepository> _logger;
    public GroupRepository(ApplicationDbContext db, ILogger<GroupRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<int> DeleteAsync(int id)
    {
        _logger.LogInformation("Request for deleting Group with Id: {Id}", 
            id);
        var group = await _db.Groups
            .Include(c => c.TechStudyUsers)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (group == null) return 0;
        _db.Groups.Remove(group);
        int res = await _db.SaveChangesAsync();
        if(res != 0)
        {
            _logger.LogInformation("Group Id: {Id} was deleted",
                id);
        }
        return res;
    }


    public async Task<IEnumerable<Group>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        return await _db.Groups
            .OrderBy(c => c.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Group?> GetByIdAsync(int id)
    {
        var group = await _db.Groups
            .Include(c => c.TechStudyUsers)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        return group;
    }

    public async Task<int> InsertAsync(Group group)
    {
        _logger.LogInformation("Adding group to db {Name}",
            group.Name);
        await _db.Groups.AddAsync(group);
        int res = await _db.SaveChangesAsync();
        if (res != 0)
        {
           _logger.LogInformation("Group {Name} was added",
                group.Name);
        }
        return res;
    }

    public async Task<int> UpdateAsync(int id, Group updated)
    {
        _logger.LogInformation("Request to update group with Id: {Id}", id);
        var group = await _db.Groups.FirstOrDefaultAsync(_ => _.Id == id);
        if(group == null) return 0;
        group.Name = updated.Name;
        group.Description = updated.Description;
        int res = await _db.SaveChangesAsync();
        if(res != 0)
        {
            _logger.LogInformation("Group was updated. {NewName}, {NewDesc}", 
                group.Name, group.Description);

        }
        return res;
    }
}
