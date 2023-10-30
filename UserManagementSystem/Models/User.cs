using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagementSystem.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Index(IsUnique = true)]
        public string UserId { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public  DateTime DateOfBirth { get; set; }
       
        // Navigation properties
        public virtual SpecialCode SpecialCode { get; set; }
        public virtual Password Password { get; set; }
    }

    public class SpecialCode
    {
        [Key, ForeignKey("User")]
        public string UserId { get; set; }
        public string Code { get; set; } // Auto-generated unique number
        public string Mobile { get; set; }
        public string Email { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }

    public class Password
    {
        [Key, ForeignKey("User")]
        public string UserId { get; set; }
        public string Salt { get; set; } // Hash of date and time
        public string HashedPassword { get; set; } // Hash of salt and special code

        // Navigation property
        public virtual User User { get; set; }
    }

}