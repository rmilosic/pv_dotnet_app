using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace project_management_app.Models
{
    public partial class ProjectCooperation
    {
        public long Id { get; set; }

        
        [DisplayName("Project ID")]
        public long ProjectId { get; set; }
        
        [DisplayName("Team ID")]
        public long TeamId { get; set; }
        
        [Required]
        [DisplayName("Assigned Date")]
        [DataType(DataType.Date)]
        public DateTime DateAssigned { get; set; }

        public virtual Projects Project { get; set; }
        public virtual Teams Team { get; set; }
    }
}
