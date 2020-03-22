using System;
using System.Collections.Generic;

namespace project_management_app.Models
{
    public partial class WorkItems
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateStarted { get; set; }
        public DateTime? DateFinished { get; set; }
        public DateTime DateDue { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Projects Project { get; set; }
    }
}
