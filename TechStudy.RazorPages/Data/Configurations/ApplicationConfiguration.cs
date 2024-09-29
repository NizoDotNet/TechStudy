using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Data.Configurations;

public class ApplicationConfiguration : IEntityTypeConfiguration<ApplicationForMembership>
{
    public void Configure(EntityTypeBuilder<ApplicationForMembership> builder)
    {
        

        builder
            .HasOne<Group>(c => c.Group)
            .WithMany(c => c.Applications);
    }
}
