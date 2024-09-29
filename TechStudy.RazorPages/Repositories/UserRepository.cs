using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TechStudy.RazorPages.Data;

namespace TechStudy.RazorPages.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<TechStudyUser> _userManager;
    private readonly ApplicationDbContext _db;
    public UserRepository(UserManager<TechStudyUser> userManager, ApplicationDbContext db)
    {
        _userManager = userManager;
        _db = db;
    }

    public async Task<bool> CreateUser(TechStudyUser user)
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

    public async Task<IEnumerable<TechStudyUser>> GetAllAsync()
    {
        return await _userManager.Users.ToListAsync();
    }

    public async Task<TechStudyUser> GetAsync(string id)
    {
        var user = await _db.Users
            .Include(c => c.Group)
            .Include(c => c.ApplicationsForMembership)
                .ThenInclude(c => c.Group)
            .Include(c => c.ApplicationsForMembership)
                .ThenInclude(c => c.ApplicationStatus)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        return user;
    }

    public async Task<bool> UpdateAsync(string id, TechStudyUser updatedUser)
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
