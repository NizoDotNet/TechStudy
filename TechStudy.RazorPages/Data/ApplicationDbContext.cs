﻿using Microsoft.AspNetCore.Identity;
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new GroupConfiguration());

            builder.Entity<Group>()
                .HasData([new(Guid.Parse("8275d12e-ef4e-4644-bd54-2778b976b9a9"), "No Group")]);

           
        }

    }
}
