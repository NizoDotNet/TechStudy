using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Repositories;

public interface IApplicationRepository
{
    Task<int> InsertAsync(ApplicationForMembership application);
    Task<int> DeleteAsync(int id, string? userId = null);
    Task<IEnumerable<ApplicationForMembership>> GetAllAsync(string? userId = null, bool? onlyinreview = null);
    Task<ApplicationForMembership> GetByIdAsync(int id);
    Task<int> UpdateAsync(int id,  ApplicationForMembership updatedApplication);
}
