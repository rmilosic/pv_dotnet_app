using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

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
        public string Name { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
        public virtual ICollection<ProjectCooperation> ProjectCooperation { get; set; }
    }
}
