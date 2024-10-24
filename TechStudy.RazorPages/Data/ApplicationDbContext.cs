using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechStudy.RazorPages.Data.Configurations;
using TechStudy.RazorPages.Entities;

namespace TechStudy.RazorPages.Data
{
    public class ApplicationDbContext 
        : IdentityDbContext<TechStudyUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<ApplicationForMembership> Applications { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new GroupConfiguration());
            builder.ApplyConfiguration(new ApplicationConfiguration());
            builder.ApplyConfiguration(new SpecializationConfiguration());

            builder.Entity<TechStudyUser>()
                .HasMany(c => c.ApplicationsForMembership)
                .WithOne(c => c.TechStudyUser)
                .HasForeignKey(c => c.TechStudyUserId)
                .IsRequired(false);

            builder.Entity<TechStudyUser>()
                .HasOne(c => c.Specialization);

            builder.Entity<Faculty>()
                .HasData([new() { Id = 1, Text = "Other" }, new() { Id = 2, Text = "Engineering"}]);

            builder
                .Entity<ApplicationStatus>()
                .HasData([new() { Id = 1, Name = "Review"}, 
                    new() {Id = 2, Name = "Accepted" }, 
                    new() { Id = 3, Name = "Rejected"}]);

        }

    }
}
