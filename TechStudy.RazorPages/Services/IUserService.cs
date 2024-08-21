using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TechStudy.RazorPages.Services;

public interface IUserService 
{
    Task<IdentityUser> GetAsync(string id);
    Task<IEnumerable<IdentityUser>> GetAllAsync();
    Task<bool> Delete(string id);
    Task<bool> UpdateAsync(string id, IdentityUser updatedUser);
    Task<bool> CreateUser(IdentityUser user);
    Task<bool> AddClaimAsync(string userId, Claim claim);
    Task<bool> RemoveClaimAsync(string userId, Claim claim);
    Task<bool> HasClaim(string userId, Claim claim);
    Task<IEnumerable<Claim>> GetClaimsAsync(string userId);
    Task<IEnumerable<IdentityUser>> GetUserAsync();
    
}
