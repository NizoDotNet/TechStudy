using System.ComponentModel.DataAnnotations;

namespace TechStudy.RazorPages.Entities;

public class Faculty 
{
    public int Id { get; set; }
    [Required]
    public string Text { get; set; } = string.Empty;
    public ICollection<Specialization> Specializations { get; set; } = [];
}
