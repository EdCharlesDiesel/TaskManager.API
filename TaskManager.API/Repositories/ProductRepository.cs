using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Context;
using TaskManager.API.Models;
using TaskManager.API.Services;

namespace TaskManager.API.Repositories
{

    public class ProjectRepository : IProjectService
    {
        readonly TaskManagerDbContext _dbContext;

        public ProjectRepository(TaskManagerDbContext dbContext)
        {
            _dbContext = dbContext?? throw new ArgumentNullException(nameof(TaskManagerDbContext));
        }      


        public async Task  AddProject(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
        }

        public void DeleteProject(Project project)
        {
            _dbContext.Projects.Remove(project);
        }

        public async Task<Project> GetProject(Guid projectId)
        {
            return await _dbContext.Projects.FirstOrDefaultAsync(p => p.ProjectId == projectId);
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }

        public async Task<bool> ProjectExists(Guid projectId)
        {
            return await _dbContext.Projects.AnyAsync(t => t.ProjectId == projectId);
        }

        public async Task UpdateProject(Project project)
        {
            // no code in this implementation
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();                    
                }

            }
        }
    }
}

