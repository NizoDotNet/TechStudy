using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Data.Configurations;

public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.HasData([new() { Id = 1, Name = "Information Technologies", FacultyId = 2}, 
            new() { Id = 2, Name = "Computer Engineering", FacultyId = 2}, 
            new() { Id = 3, Name = "Other", FacultyId = 1}]);

        builder.HasOne(c => c.Faculty)
            .WithMany(c => c.Specializations);
    }

}
