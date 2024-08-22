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
    Task<bool> AddClaimAsync(IdentityUser user, Claim claim);
    Task<bool> ReplaceClaimAsync(string userId, Claim oldClaim, Claim newClaim);
    Task<bool> ReplaceClaimAsync(IdentityUser user, Claim oldClaim, Claim newClaim);
    Task<bool> RemoveClaimAsync(string userId, Claim claim);
    Task<bool> HasClaim(string userId, string type, string value);
    Task<bool> HasClaim(string userId, string type);
    Task<Claim> GetClaimAsync(string userId, string type);
    Task<Claim> GetClaimAsync(IdentityUser user, string type);
    Task<IEnumerable<Claim>> GetClaimsAsync(string userId);
    Task<IEnumerable<Claim>> GetClaimsAsync(IdentityUser user);
    Task<IEnumerable<IdentityUser>> GetUsersAsync();
    
}
