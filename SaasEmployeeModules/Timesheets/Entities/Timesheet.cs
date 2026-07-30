using System.ComponentModel.DataAnnotations;

namespace Timesheets.Entities
{
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
        [MaxLength(20)]
        public string Status { get; set; } = "DRAFT";

        public decimal TotalHours { get; set; }

        public DateTime? SubmittedOn { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TimesheetEntry> Entries { get; set; } = new List<TimesheetEntry>();
    }
}