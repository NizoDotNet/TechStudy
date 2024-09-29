using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Data.Configurations;

public class ApplicationConfiguration : IEntityTypeConfiguration<ApplicationForMembership>
{
    public void Configure(EntityTypeBuilder<ApplicationForMembership> builder)
    {
        builder
            .HasOne<TechStudyUser>(c => c.TechStudyUser)
            .WithOne(c => c.ApplicationForMembership)
            .IsRequired(false);

        builder
            .HasOne<Group>(c => c.Group)
            .WithMany(c => c.Applications);
    }
}
