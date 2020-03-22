using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;


namespace project_management_app.Models
{
    public partial class Employees
    {
        public Employees()
        {
            Projects = new HashSet<Projects>();
            WorkItems = new HashSet<WorkItems>();
        }

        public long Id { get; set; }
        
        [Required]
        [StringLength(100)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [DisplayName("Middle Name")] 
        public string Middlename { get; set; }
        
        [Required]
        [StringLength(100)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [Remote(action: "VerifyEmail", controller: "Employees")]
        [StringLength(254)]
        [DisplayName("Email Address")]
        // [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        public long TeamId { get; set; }

        public virtual Teams Team { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
        public virtual ICollection<WorkItems> WorkItems { get; set; }
    }
}
