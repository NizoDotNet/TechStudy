﻿
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
    public string? AboutMe { get; set; } 
}
