namespace TechStudy.RazorPages.Helpers.Options;

public class EmailOption
{
    public string SMPTPServer { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
