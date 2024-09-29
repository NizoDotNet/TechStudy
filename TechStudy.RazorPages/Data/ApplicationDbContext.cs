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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new GroupConfiguration());

            builder.ApplyConfiguration(new ApplicationConfiguration());

            builder.Entity<Group>()
                .HasData([new() { Id = 1, Description = "Heçbir qrupda iştirak etmirsiz.", Name = "No Group"}]);

           
        }

    }
}
