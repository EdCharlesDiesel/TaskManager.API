using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TaskManager.API.Models;

namespace TaskManager.API.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasData(
               new Project
               {
                   ProjectId = new Guid("922388FD-0461-4CB9-B9CB-A3A0CC21036C"),
                   ProjectName = "Project A",
                   DateOfStart = DateTime.Now,
                   TeamSize = 5                
                }
                    );

            modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {              
                //Id = "689E8923-05C1-4C4C-9D17-6C45AA6840D2",
                Name = "Admin",
            });


            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    EmailConfirmed= true,
                    //Id = "DDE1FDF7-966E-4386-8FE7-087507CFAC20",
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    PasswordHash = "Admin123#"
                });
        }
    }
}
