using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Data.Configurations
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasMany(c => c.TechStudyUsers)
                .WithOne(c => c.Group)
                .HasForeignKey(c => c.GroupId)
                .IsRequired(false);
        }
    }
}
