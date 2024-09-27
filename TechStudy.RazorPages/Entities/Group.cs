using Microsoft.AspNetCore.Identity;
using TechStudy.RazorPages.Data;

namespace TechStudy.RazorPages.Entities;

public class Group
{
    public Group(Guid id, string description)
    {
        Description = description;
        Id = Id;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = null!;
    public ICollection<TechStudyUser> TechStudyUsers { get; private set; } = [];
}
