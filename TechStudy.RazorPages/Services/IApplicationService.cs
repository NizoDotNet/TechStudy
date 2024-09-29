using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Services;

public interface IApplicationService
{
    Task<ApplicationForMembership> GetByIdAsync(int id);
    Task<IEnumerable<ApplicationForMembership>> GetAllAsync();
    Task<int> DeleteAsync(int id);
    Task<int> InsertAsync(ApplicationForMembership applicationForMembership);
}
