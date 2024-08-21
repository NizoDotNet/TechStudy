using Microsoft.AspNetCore.Identity;

namespace TechStudy.RazorPages.Repositories;

public interface IUserRepository
{
    Task<IdentityUser> GetAsync(string id);
    Task<IEnumerable<IdentityUser>> GetAllAsync();
    Task<bool> Delete(string id);
    Task<bool> UpdateAsync(string id, IdentityUser updatedUser);
    Task<bool> CreateUser(IdentityUser user); 
}
