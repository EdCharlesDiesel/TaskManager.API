using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Models;

namespace TaskManager.API.Identity
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
