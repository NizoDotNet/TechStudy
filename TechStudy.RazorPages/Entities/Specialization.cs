using System.ComponentModel.DataAnnotations;

namespace TechStudy.RazorPages.Entities;

public class Specialization
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    public int FacultyId { get; set; } 
    public Faculty? Faculty { get; set; }
}
