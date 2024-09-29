using TechStudy.RazorPages.Data;
using TechStudy.RazorPages.Entities;
using TechStudy.RazorPages.Repositories;

namespace TechStudy.RazorPages.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IUserService _userService;
    public ApplicationService(IApplicationRepository applicationRepository, IUserService userService)
    {
        _applicationRepository = applicationRepository;
        _userService = userService;
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _applicationRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ApplicationForMembership>> GetAllAsync()
    {
        return await _applicationRepository.GetAllAsync();
    }

    public async Task<ApplicationForMembership> GetByIdAsync(int id)
    {
        return await _applicationRepository.GetByIdAsync(id);
    }

    public async Task<int> InsertAsync(ApplicationForMembership applicationForMembership)
    {
        var user = await _userService.GetAsync(applicationForMembership.TechStudyUserId);
        if (user == null)
            return 0;
        user.ApplicationId = applicationForMembership.Id;
        return await _applicationRepository.InsertAsync(applicationForMembership);
    }
}
