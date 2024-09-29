using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Repositories;

public interface IApplicationRepository
{
    Task<int> InsertAsync(ApplicationForMembership application);
    Task<int> DeleteAsync(int id);
    Task<IEnumerable<ApplicationForMembership>> GetAllAsync();
    Task<ApplicationForMembership> GetByIdAsync(int id);
    
}
