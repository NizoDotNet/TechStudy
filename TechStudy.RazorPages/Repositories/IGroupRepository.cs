using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Repositories;

public interface IGroupRepository 
{
    Task<IEnumerable<Group>> GetAllAsync();
    Task<Group> GetByIdAsync(int id);
    Task<int> InsertAsync(Group group);
    Task<int> UpdateAsync(int Id,  Group updated);
    Task<int> DeleteAsync(int id);
}
