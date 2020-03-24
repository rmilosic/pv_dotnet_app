using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace project_management_app.Models
{
    public partial class WorkItems
    {
        public long Id { get; set; }

        
        public string Name { get; set; }
        public string Description { get; set; }

        // TODO: model validation allow only projects id in which employees team is involved in
        // i.e. allow project ID assignment only of employees working on the project

        [DisplayName("Project ID")]
        [Remote(action: "ValidateCompositeKey", controller: "WorkItems", AdditionalFields="EmployeeId")]
        public long ProjectId { get; set; }
        
        [DisplayName("Employee ID")]
        public long EmployeeId { get; set; }

        //  TODO: default: now
        // [DisplayName("Date Created")]
        // [Required]
        // public DateTime DateCreated { get; set; }


        private DateTime _createdOn = DateTime.Now;
        public DateTime DateCreated
        {
                    get
                    {
                        return (_createdOn == DateTime.MinValue) ? DateTime.Now : _createdOn;
                    }
                    set { _createdOn = value; }
        }

        [DisplayName("Date Started")]
        [Required]
        public DateTime? DateStarted { get; set; }

        [DisplayName("Date Finished")]
        public DateTime? DateFinished { get; set; }

        [DisplayName("Date Due")]
        [Required]
        public DateTime DateDue { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Projects Project { get; set; }
    }
}
