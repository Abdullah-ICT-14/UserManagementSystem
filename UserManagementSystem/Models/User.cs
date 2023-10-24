using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace UserManagementSystem.Models
{
    public class User
    {
        [Key]
         [Required]
            public string UserId { get; set; }

           
            public string EmployeeId { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            public string Mobile { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public DateTime DateOfBirth { get; set; }

            [Required]
            public string Password { get; set; }
        
    }
}