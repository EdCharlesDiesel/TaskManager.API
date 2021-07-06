using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TaskManager.API.Context;
using TaskManager.API.Models;
using TaskManager.API.Services;
using System.Linq;
using TaskManager.API.Identity;

namespace TaskManager.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [EnableCors("TaskManagerPolicy")]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectRepository;
        //private readonly TaskManagerDbContext _context;
        private readonly ApplicationDbContext _context;

        public ProjectsController(IProjectService projectRepository,  ApplicationDbContext context)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _context= context ?? throw new ArgumentNullException(nameof(context));            
        }

        //[HttpGet()]
        //public ActionResult<IEnumerable<Project>> Search(string searchBy,string seachText)
        //{

        //    List<Project> projects = null;
        //    if (searchBy == "ProjectId")
        //    {
        //        projects = _context.Projects.Where(temp => temp.ProjectId.ToString().Contains(seachText)).ToList();
        //    }
        //    else if (searchBy == "ProjectName")
        //    {
        //        projects = _context.Projects.Where(temp => temp.ProjectName.ToString().Contains(seachText)).ToList();
        //    }

        //    if (searchBy == "DateOfStart")
        //    {
        //        projects = _context.Projects.Where(temp => temp.ProjectId.ToString().Contains(seachText)).ToList();
        //    }

        //    if (searchBy =="TeamSize")
        //    {
        //        projects = _context.Projects.Where(temp => temp.ProjectId.ToString().Contains(seachText)).ToList();
        //    }

        //    return projects;
        //}

        [HttpGet()]
        public IActionResult GetProjects()
        {

            var projectsFromRepo = _projectRepository.GetAllProjects();

            return Ok(projectsFromRepo);
        }

        [HttpGet("{projectId}", Name = "GetProject")]
        public IActionResult GetProject(Guid projectId)
        {
            var projectFromRepo = _projectRepository.GetProjectData(projectId);

            if (projectFromRepo == null)
            {
                return NotFound();
            }

            return Ok(projectFromRepo);
        }

        [HttpPost()]
        public IActionResult CreateProject([FromBody] Project projectToAdd)
        {
            _projectRepository.AddProject(projectToAdd);

            _projectRepository.Save();

            return CreatedAtRoute("GetProject",
                new { projectId = projectToAdd.ProjectId },
                projectToAdd);
        }

        [HttpDelete("{projectId}")]        
        public IActionResult DeleteProject(Guid projectId)
        {
            var projectFromRepo = _projectRepository.GetProjectData(projectId);

            if (projectFromRepo == null)
            {
                return NotFound();
            }

             _projectRepository.DeleteProject(projectId);

            _projectRepository.Save();

            return NoContent();
        }

        [HttpPut("{projectId}")]
        public IActionResult UpdateProject([FromBody] Project projectForUpdate)
        {
            var projectFromRepo = _projectRepository.GetProjectData(projectForUpdate.ProjectId);
            if (projectFromRepo == null)
            {
                return NotFound();
            }
            projectFromRepo.ProjectName = projectForUpdate.ProjectName;
            projectFromRepo.DateOfStart= projectForUpdate.DateOfStart;
            projectFromRepo.TeamSize = projectForUpdate.TeamSize;

            _projectRepository.UpdateProject(projectFromRepo);

            _projectRepository.Save();

            return NoContent();
        }
    }
}