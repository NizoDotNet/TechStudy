using Microsoft.EntityFrameworkCore;
using TechStudy.RazorPages.Data;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _db;

    public ApplicationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<int> DeleteAsync(int id, string? userId = null)
    {
        var query = _db.Applications.AsQueryable();
        if(userId != null)
        {
            query = query.Where(c => c.TechStudyUserId  == userId);
        }
        var app = await query.FirstOrDefaultAsync(c => c.Id == id);
        if(app == null)
        {
            return -1;
        }

        _db.Applications.Remove(app);
        return await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<ApplicationForMembership>> GetAllAsync(string? userId = null)
    {
        var query = _db.Applications.AsSplitQuery();
        if(userId != null)
        {
            query = query.Where(c => c.TechStudyUserId == userId);
        }
        return await query
            .Include(c => c.TechStudyUser)
                .ThenInclude(c => c.Group)
            .Include(c => c.Group)
            .Include(c => c.ApplicationStatus)
            .OrderBy(c => c.DateTime)
            .OrderBy(c => c.ApplicationStatusId)
            .AsNoTracking()
            .ToListAsync(); 
    }

    public async Task<ApplicationForMembership?> GetByIdAsync(int id)
    {
        return await _db.Applications
            .Include(c => c.TechStudyUser)
                .ThenInclude(c => c.Group)
            .Include(c => c.Group)
            .Include(c => c.ApplicationStatus)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<int> InsertAsync(ApplicationForMembership application)
    {
        await _db.Applications.AddAsync(application);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(int id, ApplicationForMembership updatedApplication)
    {
        var app = await _db.Applications.FirstOrDefaultAsync(c => c.Id == id);
        if (app is null)
            return 0;

        app.ApplicationStatusId = updatedApplication.ApplicationStatusId;
        return await _db.SaveChangesAsync();
    }
}
