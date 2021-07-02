using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Models;

namespace TaskManager.API.Context
{
    public class TaskManagerDbContext: DbContext
    {
        public DbSet<Project> Projects { get; set; }

        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options)
        {
        }

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
        }

    }
}
