using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TechStudy.RazorPages.Data;

namespace TechStudy.RazorPages.Services;

public interface IUserService 
{
    Task<TechStudyUser> GetAsync(string id);
    Task<IEnumerable<TechStudyUser>> GetAllAsync();
    Task<bool> Delete(string id);
    Task<bool> UpdateAsync(string id, TechStudyUser updatedUser);
    Task<bool> CreateUser(TechStudyUser user);
    Task<bool> AddClaimAsync(string userId, Claim claim);
    Task<bool> AddClaimAsync(TechStudyUser user, Claim claim);
    Task<bool> ReplaceClaimAsync(string userId, Claim oldClaim, Claim newClaim);
    Task<bool> ReplaceClaimAsync(TechStudyUser user, Claim oldClaim, Claim newClaim);
    Task<bool> RemoveClaimAsync(string userId, Claim claim);
    Task<bool> HasClaim(string userId, string type, string value);
    Task<bool> HasClaim(string userId, string type);
    Task<Claim> GetClaimAsync(string userId, string type);
    Task<Claim> GetClaimAsync(TechStudyUser user, string type);
    Task<IEnumerable<Claim>> GetClaimsAsync(string userId);
    Task<IEnumerable<Claim>> GetClaimsAsync(TechStudyUser user);
    
}
