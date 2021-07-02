using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.API.Models;

namespace TaskManager.API.Services
{
    public interface IProjectService
    {
        Task AddProject(Project project);
        void DeleteProject(Project project);
        Task<Project> GetProject(Guid projectId);
        Task<IEnumerable<Project>> GetProjects();                
        Task<bool> SaveAsync();
        Task<bool> ProjectExists(Guid projectId);
        Task UpdateProject(Project project);        
    }
    
}
