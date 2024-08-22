using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.Security.Claims;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Services;

public class UserService : IUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserRepository _userRepository;

    public UserService(UserManager<IdentityUser> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<bool> AddClaimAsync(string userId, Claim claim)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user is null)
        {
            return false;
        }
        var res = await _userManager.AddClaimAsync(user, claim);
        return res.Succeeded;
    }

    public async Task<bool> AddClaimAsync(IdentityUser user, Claim claim)
    {
        var res = await _userManager.AddClaimAsync(user, claim);
        return res.Succeeded;
    }
    public async Task<bool> ReplaceClaimAsync(IdentityUser user, Claim oldClaim, Claim newClaim)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        if(userClaims.Where(c => c.Type == oldClaim.Type && c.Value == oldClaim.Value ).Any())
        {
            var res = await _userManager.ReplaceClaimAsync(user, oldClaim, newClaim);
            return res.Succeeded;
        }
        return false;
    }

    public async Task<bool> CreateUser(IdentityUser user)
    {
        return await _userRepository.CreateUser(user);
    }

    public async Task<bool> Delete(string id)
    {
        return await _userRepository.Delete(id);
    }

    public async Task<IEnumerable<IdentityUser>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<IdentityUser> GetAsync(string id)
    {
        var user = await _userRepository.GetAsync(id);
        return user;
    }

    public async Task<Claim> GetClaimAsync(string userId, string type)
    {
        var user = await _userRepository.GetAsync(userId);
        if(user is null)
        {
            return new(type, string.Empty);
        }
        var userClaims = await _userManager.GetClaimsAsync(user); 
        var claim = userClaims.Where(c => c.Type == type).FirstOrDefault();
        return claim ?? new(type, string.Empty);
    }

    public async Task<Claim> GetClaimAsync(IdentityUser user, string type)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var claim = userClaims.Where(c => c.Type == type).FirstOrDefault();
        return claim ?? new(type, string.Empty);
    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync(string userId)
    {
        var user = await _userRepository.GetAsync(userId);

        return await _userManager.GetClaimsAsync(user);
    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync(IdentityUser user)
    {
        return await _userManager.GetClaimsAsync(user);
    }

    public async Task<IEnumerable<IdentityUser>> GetUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<bool> HasClaim(string userId, Claim claim)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user is null)
        {
            return false;
        }
        var userClaims = await _userManager.GetClaimsAsync(user);
        return userClaims.Any(c => c.Type == claim.Type && c.Value == claim.Value);
    }

    public async Task<bool> HasClaim(string userId, string type, string value)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user is null)
        {
            return false;
        }
        var userClaims = await _userManager.GetClaimsAsync(user);
        return userClaims.Any(c => c.Type == type && c.Value == value);
    }

    public async Task<bool> HasClaim(string userId, string type)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user is null)
        {
            return false;
        }
        var userClaims = await _userManager.GetClaimsAsync(user);
        return userClaims.Any(c => c.Type == type);
    }

    public async Task<bool> RemoveClaimAsync(string userId, Claim claim)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user is null)
        {
            return false;
        }
        var res = await _userManager.RemoveClaimAsync(user, claim);
        return res.Succeeded;
    }


    public async Task<bool> UpdateAsync(string id, IdentityUser updatedUser)
    {
        return await _userRepository.UpdateAsync(id, updatedUser);
    }

    public async Task<bool> ReplaceClaimAsync(string userId, Claim oldClaim, Claim newClaim)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) 
        {
            return false;
        }
        return await ReplaceClaimAsync(user, oldClaim, newClaim);
    }
}
