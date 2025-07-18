using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.DateTimeCalc.Models
{
    [Table("GIBSDateTimeCalc")]
    public class DateTimeCalc : IAuditable
    {
        [Key]
        public int DateTimeCalcId { get; set; }
        public int ModuleId { get; set; } // Foreign key to the Module table
        public string Name { get; set; }
        public DateTime? StartDate { get; set; } = null; // Default to null, can be set later  
        public DateTime? EndDate { get; set; } = null; // Default to null, can be set later
        public bool ShowYears { get; set; } = false;
        public bool ShowMonths { get; set; } = false;
        public bool ShowWeeks { get; set; } = false;
        public bool ShowDays { get; set; } = false;
        public bool ShowHours { get; set; } = false;
        public bool ShowMinutes { get; set; } = false;
        public bool ShowSeconds { get; set; } = false;
        public string Template { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
