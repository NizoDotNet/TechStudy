
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Data;

public class TechStudyUser : IdentityUser
{
    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string FirstName { get; set; } = null!;
    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string SecondName { get; set; } = null!;
    public string Faculty { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public Guid GroupId { get; set; } 
    public Group Group { get; set; } 
}
