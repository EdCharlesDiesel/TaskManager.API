using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManager.API.Models
{
    /// <summary>
    /// Entity for Project
    /// </summary>
    public class Project
    {
        /// <summary>
        /// ProjectId, unique id for the project.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ProjectId { get; set; }

        /// <summary>
        /// Project name the name of the project
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Date of start the start date of the project
        /// </summary>
        public DateTime DateOfStart { get; set; }

        /// <summary>
        /// The size of the team.
        /// </summary>
        public int TeamSize { get; set; }
    }
}
