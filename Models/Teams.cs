using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace project_management_app.Models
{
    public partial class Teams
    {
        public Teams()
        {
            Employees = new HashSet<Employees>();
            ProjectCooperation = new HashSet<ProjectCooperation>();
        }

        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        [Remote(action: "VerifyUniqueName", controller: "Teams")]
        public string Name { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<ProjectCooperation> ProjectCooperation { get; set; }
    }
}
