using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.API.Services
{
    public interface IProjectService
    {     
        List<Project> GetAllProjects();
        int AddProject(Project project);
        int UpdateProject(Project project);
        Project GetProjectData(int projectId);
        string DeleteProject(int projectId);
        bool Save();
    }    
}
