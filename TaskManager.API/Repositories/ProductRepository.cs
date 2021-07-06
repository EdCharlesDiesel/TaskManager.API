using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.API.Context;
using TaskManager.API.Identity;
using TaskManager.API.Models;
using TaskManager.API.Services;

namespace TaskManager.API.Repositories
{

    public class ProjectRepository : IProjectService, IDisposable
    {
        //private readonly TaskManagerDbContext _dbContext;
        private readonly ApplicationDbContext _dbContext;

        public ProjectRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext?? throw new ArgumentNullException(nameof(_dbContext));
        }

        public Guid AddProject(Project project)
        {
            try
            {
                project.ProjectId = Guid.NewGuid();
                _dbContext.Projects.Add(project);
                _dbContext.SaveChanges();

                return project.ProjectId;
            }
            catch
            {
                throw;
            }
        }

        public string DeleteProject(Guid projectId)
        {
            try
            {
                Project project = _dbContext.Projects.Find(projectId);
                _dbContext.Projects.Remove(project);
                _dbContext.SaveChanges();

                return project.ProjectName;
            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public List<Project> GetAllProjects()
        {
            try
            {
                return _dbContext.Projects.ToList();
            }

            catch
            {
                throw;
            }
        }

        public Project GetProjectData(Guid projectId)
        {
            try
            {
                Project project = _dbContext.Projects.FirstOrDefault(x => x.ProjectId == projectId);
                if (project != null)
                {
                    _dbContext.Entry(project).State = EntityState.Detached;
                    return project;
                }
                return null;
            }
            catch
            {
                throw;
            }

        }

        public Guid UpdateProject(Project project)
        {
            try
            {
                Project oldProjectData = GetProjectData(project.ProjectId);

                if (oldProjectData.ProjectName != null)
                {
                    if (project.ProjectName == null)
                    {
                        project.ProjectName = oldProjectData.ProjectName;
                    }
                }

                _dbContext.Entry(project).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return new Guid();
            }
            catch
            {
                throw;
            }
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() >= 0;
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

