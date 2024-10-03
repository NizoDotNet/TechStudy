using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Services;

public interface IApplicationService
{
    Task<ApplicationForMembership> GetByIdAsync(int id);
    Task<IEnumerable<ApplicationForMembership>> GetAllAsync(string? userId = null);
    Task<int> DeleteAsync(int id, string? userId = null);
    Task<int> InsertAsync(ApplicationForMembership applicationForMembership);
    Task<int> SetNewStatus(int id, ApplicationStatusId applicationStatus);
}
