
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
    public int? GroupId { get; set; } = 1;
    public Group? Group { get; set; }
    public int? SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }
    public ICollection<ApplicationForMembership> ApplicationsForMembership { get; set; } = [];
}
