using Microsoft.AspNetCore.Identity;
using TechStudy.RazorPages.Data;

namespace TechStudy.RazorPages.Repositories;

public interface IUserRepository
{
    Task<TechStudyUser> GetAsync(string id);
    Task<IEnumerable<TechStudyUser>> GetAllAsync();
    Task<bool> Delete(string id);
    Task<bool> UpdateAsync(string id, TechStudyUser updatedUser);
    Task<bool> CreateUser(TechStudyUser user); 
}
