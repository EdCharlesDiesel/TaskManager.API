﻿using TaskManager.Models;
using System.Collections.Generic;

namespace TaskManager.ViewModels
{
    public class SignUpViewModel
    {
        public PersonFullName PersonName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string DateOfBirth { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public int CountryID { get; set; }
        public bool ReceiveNewsLetters { get; set; }
        public List<Skill> Skills { get; set; }
    }

    public class PersonFullName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}


