using System;
using System.Collections.Generic;
using TaskManager.API.Models;

namespace TaskManager.API.Services
{
    public interface IProjectService
    {     
        List<Project> GetAllProjects();
        Guid AddProject(Project project);
        Guid UpdateProject(Project project);
        Project GetProjectData(Guid projectId);
        string DeleteProject(Guid projectId);
        bool Save();
    }    
}
