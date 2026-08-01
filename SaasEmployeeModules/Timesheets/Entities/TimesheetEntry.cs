using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Timesheets.Entities
{
    [Table("timesheet_entries")]
    public class TimesheetEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TimesheetId { get; set; }

        [ForeignKey(nameof(TimesheetId))]
        public Timesheet Timesheet { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Day { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ProjectName { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Task { get; set; } = string.Empty;

        [Required]
        public decimal Hours { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}