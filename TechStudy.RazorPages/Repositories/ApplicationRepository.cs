using Microsoft.EntityFrameworkCore;
using TechStudy.RazorPages.Data;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<ApplicationRepository> _logger;
    public ApplicationRepository(ApplicationDbContext db, ILogger<ApplicationRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<int> DeleteAsync(int id, string? userId = null)
    {
        _logger.LogInformation("Request for deleting application {Id}",
            id);
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
        int res = await _db.SaveChangesAsync();
        if(res != 0)
        {
            _logger.LogInformation("Application {Id} was deleted",
                id);
        }
        return res;
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
        _logger.LogInformation("Request for adding application to db {TechStudyUserId}",
            application.TechStudyUserId);
        await _db.Applications.AddAsync(application);
        int res = await _db.SaveChangesAsync();
        if(res != 0)
        {
            _logger.LogInformation("Application was added.");
        }
        return res;
    }

    public async Task<int> UpdateAsync(int id, ApplicationForMembership updatedApplication)
    {
        _logger.LogInformation("Request for updating application with Id: {Id}", id);
        var app = await _db.Applications.FirstOrDefaultAsync(c => c.Id == id);
        if (app is null)
            return 0;

        app.ApplicationStatusId = updatedApplication.ApplicationStatusId;
        int res = await _db.SaveChangesAsync();
        if(res != 0)
        {
            _logger.LogInformation("Changed application status Id to {applicationStatusId}",
                updatedApplication.ApplicationStatusId);
        }
        return res;
    }
}
