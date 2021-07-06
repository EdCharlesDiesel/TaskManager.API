using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.API.viewModels
{
    public class LoginViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
