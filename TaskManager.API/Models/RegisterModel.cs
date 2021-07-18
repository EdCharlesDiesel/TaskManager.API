using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int UserTypeId { get; set; }

        //public DateTime DateOfBirth { get; set; }

        //public string Gender { get; set; }

        //public int CountryID { get; set; }

        //public bool ReceiveNewsLetters { get; set; }

        //public string Mobile { get; set; }

        //public List<Skill> Skills { get; set; } = new List<Skill>();

    }
}
