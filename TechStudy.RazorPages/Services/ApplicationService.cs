using Microsoft.EntityFrameworkCore;
using TechStudy.RazorPages.Data;
using TechStudy.RazorPages.Entities;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IUserService _userService;
    private readonly ApplicationDbContext _db;
    public ApplicationService(IApplicationRepository applicationRepository, 
        IUserService userService, 
        ApplicationDbContext db)
    {
        _applicationRepository = applicationRepository;
        _userService = userService;
        _db = db;
    }

    public async Task<int> DeleteAsync(int id, string? userId = null)
    {
        return await _applicationRepository.DeleteAsync(id, userId);
    }

    public async Task<IEnumerable<ApplicationForMembership>> GetAllAsync(string? userId = null, bool? onlyinreview = null)
    {
        return await _applicationRepository.GetAllAsync(userId, onlyinreview);
    }

    public async Task<ApplicationForMembership> GetByIdAsync(int id)
    {
        return await _applicationRepository.GetByIdAsync(id);
    }

    public async Task<int> InsertAsync(ApplicationForMembership applicationForMembership)
    {
        var user = await _userService.GetAsync(applicationForMembership.TechStudyUserId);
        if (user == null)
            return 0;
        return await _applicationRepository.InsertAsync(applicationForMembership);
    }

    public async Task<int> SetNewStatus(int id, ApplicationStatusId applicationStatus)
    {
        var application = await _applicationRepository.GetByIdAsync(id);
        if (application is null)
            return 0;
        
        application.ApplicationStatusId = (int)applicationStatus;
        var res = await _applicationRepository.UpdateAsync(id, application);

        if (applicationStatus == ApplicationStatusId.Accepted)
        {
            var user = await _db.Users
                .Include(c => c.ApplicationsForMembership)
                .FirstOrDefaultAsync(c => c.Id == application.TechStudyUserId);
            if (user == null)
            {
                return 0;
            }
            user.GroupId = application.GroupId;
            await _db.Applications
                .Where(c => c.TechStudyUserId == user.Id)
                .Where(c => c.Id != application.Id)
                .ExecuteDeleteAsync();
            res += await _db.SaveChangesAsync();
        }
        
        return res;
    }
}
