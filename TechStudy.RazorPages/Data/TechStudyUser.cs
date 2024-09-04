
using Microsoft.AspNetCore.Identity;

namespace TechStudy.RazorPages.Data;

public class TechStudyUser : IdentityUser
{
    public string Test { get; set; } = "Test";
}
