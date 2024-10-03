using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Services;

public interface IGroupService
{
    Task<IEnumerable<Group>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<Group> GetByIdAsync(int id);
    Task<int> InsertAsync(Group group);
    Task<int> UpdateAsync(int Id, Group updated);
    Task<int> DeleteAsync(int id);
    Task<int> RemoveAccount(int groupId, string userId);
}
