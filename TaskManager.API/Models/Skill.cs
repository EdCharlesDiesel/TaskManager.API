using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.API.Authentication;

namespace TaskManager.API.Models
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }
        public string SkillName { get; set; }
        public string SkillLevel { get; set; }
        public string Id { get; set; }

        [ForeignKey("Id")]
        public virtual UserMaster ApplicationUser { get; set; }
    }
}


