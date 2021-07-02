using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Models;
using TaskManager.API.Services;

namespace TaskManager.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectRepository;

        public ProjectsController(IProjectService projectRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        [HttpGet()]
        public IActionResult GetProjects()
        {

            var projectsFromRepo = _projectRepository.GetProjects();

            return Ok(projectsFromRepo);
        }

        [HttpGet("{projectId}", Name = "GetProject")]
        public IActionResult GetProject(Guid projectId)
        {
            var projectFromRepo = _projectRepository.GetProject(projectId);

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

            _projectRepository.SaveAsync();

            return CreatedAtRoute("GetProject",
                new { projectId = projectToAdd.ProjectId },
                projectToAdd);
        }

        [HttpDelete("{projectId}")]        
        public IActionResult DeleteProject(Guid projectId)
        {
            var projectFromRepo = _projectRepository.GetProject(projectId);

            if (projectFromRepo == null)
            {
                return NotFound();
            }

             //_projectRepository.DeleteProject(projectFromRepo);

            _projectRepository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{projectId}")]
        public IActionResult UpdateProject(Guid projectId,
            [FromBody] Project projectForUpdate)
        {
            var projectFromRepo = _projectRepository.GetProject(projectId);
            if (projectFromRepo == null)
            {
                return NotFound();
            }            

            //_projectRepository.UpdateProject(projectFromRepo);

            _projectRepository.SaveAsync();

            return NoContent();
        }
    }
}