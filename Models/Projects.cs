using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace project_management_app.Models
{
    public partial class Projects
    {
        public Projects()
        {
            ProjectCooperation = new HashSet<ProjectCooperation>();
            WorkItems = new HashSet<WorkItems>();
        }


        public long Id { get; set; }
        

        [Required]
        [Remote(action: "VerifyUniqueName", controller: "Projects")]
        [StringLength(100)]
        [DisplayName("Project Name")]
        public string Name { get; set; }
        
        [DisplayName("Project Manager ID")]
        public long ProjectManagerId { get; set; }

        [DisplayName("Date Start")]
        public DateTime DateStart { get; set; }
        
        [DisplayName("Date Due")]
        public DateTime DateDue { get; set; }

        public virtual Employees ProjectManager { get; set; }
        public virtual ICollection<ProjectCooperation> ProjectCooperation { get; set; }
        public virtual ICollection<WorkItems> WorkItems { get; set; }
    }
}
