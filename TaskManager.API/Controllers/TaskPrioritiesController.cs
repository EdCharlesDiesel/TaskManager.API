using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskManager.API.Contexts;
using TaskManager.API.Models;

namespace TaskManager.API.Controllers
{
    public class TaskPrioritiesController : Controller
    {
        private TaskManagerDbContext db;

        public TaskPrioritiesController(TaskManagerDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("api/taskpriorities")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public List<TaskPriority> Get()
        {
            List<TaskPriority> taskPriorities = db.TaskPriorities.ToList();
            return taskPriorities;
        }

        [HttpGet]
        [Route("api/taskpriorities/searchbytaskpriorityid/{TaskPriorityID}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetByTaskPriorityID(int TaskPriorityID)
        {
            TaskPriority taskPriority = db.TaskPriorities.Where(temp => temp.TaskPriorityID == TaskPriorityID).FirstOrDefault();
            if (taskPriority != null)
            {
                return Ok(taskPriority);
            }
            else
                return NoContent();
        }

        [HttpPost]
        [Route("api/taskpriorities")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public TaskPriority Post([FromBody] TaskPriority taskPriority)
        {
            db.TaskPriorities.Add(taskPriority);
            db.SaveChanges();

            TaskPriority existingTaskPriority = db.TaskPriorities.Where(temp => temp.TaskPriorityID == taskPriority.TaskPriorityID).FirstOrDefault();
            return taskPriority;
        }

        [HttpPut]
        [Route("api/taskpriorities")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public TaskPriority Put([FromBody] TaskPriority project)
        {
            TaskPriority existingTaskPriority = db.TaskPriorities.Where(temp => temp.TaskPriorityID == project.TaskPriorityID).FirstOrDefault();
            if (existingTaskPriority != null)
            {
                existingTaskPriority.TaskPriorityName = project.TaskPriorityName;
                db.SaveChanges();
                return existingTaskPriority;
            }
            else
            {
                return null;
            }
        }

        //[HttpDelete]
        //[Route("api/taskpriorities")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public int Delete(int TaskPriorityID)
        //{

        //}
        //public int IActionResult Index()
        //{
        //    TaskPriority existingTaskPriority = db.TaskPriorities.Where(temp => temp.TaskPriorityID == TaskPriorityID).FirstOrDefault();
        //    if (existingTaskPriority != null)
        //    {
        //        db.TaskPriorities.Remove(existingTaskPriority);
        //        db.SaveChanges();
        //        return TaskPriorityID;
        //    }
        //    else
        //    {
        //        return -1;
        //    }
        //    return View();
        //}
    }
}
