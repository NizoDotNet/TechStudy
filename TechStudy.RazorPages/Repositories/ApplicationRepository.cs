﻿using Microsoft.EntityFrameworkCore;
using TechStudy.RazorPages.Data;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationDbContext _db;

    public ApplicationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var application = await _db.Applications.FirstOrDefaultAsync(c => c.Id == id);
        if (application == null)
            return 0;
        _db.Applications.Remove(application);
        return await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<ApplicationForMembership>> GetAllAsync()
    {
        return await _db.Applications
            .AsNoTracking()
            .ToListAsync(); 
    }

    public async Task<ApplicationForMembership> GetByIdAsync(int id)
    {
        return await _db.Applications.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<int> InsertAsync(ApplicationForMembership application)
    {
        await _db.Applications.AddAsync(application);
        return await _db.SaveChangesAsync();
    }
}
