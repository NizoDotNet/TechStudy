using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TechStudy.Models.User;

public class TechStudyUser : IdentityUser
{
    [Required]
    [MinLength(3)]
    public string FirstName { get; set; } = null!;
    [Required]
    [MinLength(3)]
    public string LastName { get; set; } = null!;
}
