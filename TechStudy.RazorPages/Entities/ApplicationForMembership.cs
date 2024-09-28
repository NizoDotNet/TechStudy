namespace TechStudy.RazorPages.Entities;

public class ApplicationForMembership
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int GroupId { get; set; }
}
