using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TechStudy.RazorPages.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserRepository(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> CreateUser(IdentityUser user)
    {
        var res = await _userManager.CreateAsync(user);
        if (!res.Succeeded) 
        { 
            return false;
        }
        return true;
    }

    public async Task<bool> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null) 
        {
            return false;   
        }
        await _userManager.DeleteAsync(user);
        return true;
    }

    public async Task<IEnumerable<IdentityUser>> GetAllAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<IdentityUser> GetAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user;
    }

    public async Task<bool> UpdateAsync(string id, IdentityUser updatedUser)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null) 
        {
            return false;
        }
        user.UserName = updatedUser.UserName;
        user.NormalizedUserName = updatedUser.NormalizedUserName;
        user.PhoneNumber = updatedUser.PhoneNumber;
        user.Email = updatedUser.Email;
        return true;
    }
}
