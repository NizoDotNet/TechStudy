using Microsoft.AspNetCore.Identity;
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

    public async Task<bool> AddClaim(string userId, Claim claim)
    {
        var user = await _userRepository.GetAsync(userId);
        if (user is null)
        {
            return false;
        }
        var res = await _userManager.AddClaimAsync(user, claim);
        return res.Succeeded;
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
        var user =  await _userRepository.GetAsync(id);
        return user;
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

    public async Task<bool> RemoveClaim(string userId, Claim claim)
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
}
