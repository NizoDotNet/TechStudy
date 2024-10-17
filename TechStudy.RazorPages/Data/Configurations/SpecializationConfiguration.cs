using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Data.Configurations;

public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.HasData([new() { Id = 1, Name = "Information Technologies"}, 
            new() { Id = 2, Name = "Computer Engineering"}, 
            new() { Id = 3, Name = "Other"}]);
    }

}
