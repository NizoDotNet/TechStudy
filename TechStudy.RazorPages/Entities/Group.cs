using Microsoft.AspNetCore.Identity;
using TechStudy.RazorPages.Data;

namespace TechStudy.RazorPages.Entities;

public class Group
{
    public int Id { get; set; } 
    public string Description { get; set; } = null!;
    public ICollection<TechStudyUser> TechStudyUsers { get; private set; } = [];
}
