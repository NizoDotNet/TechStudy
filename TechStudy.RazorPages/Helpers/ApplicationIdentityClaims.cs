using System.Security.Claims;

namespace TechStudy.RazorPages.Helpers;

public class ApplicationIdentityClaims
{
    private string Role = "Role";
    private string FirstName = "FirstName";
    private string SecondName = "SecondName";
    public Claim CreateFirstName(string name) => new Claim(FirstName, name);
    public Claim CreateSecondName(string name) => new Claim(SecondName, name);
    public Claim UserRole() => new Claim(Role, "User");
    public Claim AdminRole() => new Claim(Role, "Admin");

}
