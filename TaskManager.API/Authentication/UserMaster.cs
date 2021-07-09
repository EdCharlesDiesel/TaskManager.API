using System;
using System.Collections.Generic;

namespace TaskManager.API.Authentication
{
    public class UserMaster
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int UserTypeId { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int CountryID { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        public Guid Id { get; set; }
    }
}
