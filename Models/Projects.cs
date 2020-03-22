using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public long ProjectManagerId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateDue { get; set; }

        public virtual Employees ProjectManager { get; set; }
        public virtual ICollection<ProjectCooperation> ProjectCooperation { get; set; }
        public virtual ICollection<WorkItems> WorkItems { get; set; }
    }
}
