using Microsoft.EntityFrameworkCore;
using TechStudy.RazorPages.Data;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly ApplicationDbContext _db;

    public GroupRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var group = await _db.Groups.FirstOrDefaultAsync(c => c.Id == id);
        if (group == null) return 0;
        _db.Groups.Remove(group);
        return await _db.SaveChangesAsync();
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
        await _db.Groups.AddAsync(group);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(int Id, Group updated)
    {
        var group = await _db.Groups.FirstOrDefaultAsync(_ => _.Id == Id);
        if(group == null) return 0;
        group.Description = updated.Description;
        return await _db.SaveChangesAsync();
    }
}
