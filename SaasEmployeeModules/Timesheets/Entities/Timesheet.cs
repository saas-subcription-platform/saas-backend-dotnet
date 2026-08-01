using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Timesheets.Common.Enums;

namespace Timesheets.Entities
{
    [Table("timesheets")]
    public class Timesheet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public DateOnly WeekStartDate { get; set; }

        [Required]
        public DateOnly WeekEndDate { get; set; }

        [Required]
        public TimesheetStatus Status { get; set; } = TimesheetStatus.Draft;

        public decimal TotalHours { get; set; }

        public DateTime? SubmittedOn { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TimesheetEntry> Entries { get; set; } = new List<TimesheetEntry>();
    }
}