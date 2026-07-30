using System.ComponentModel.DataAnnotations;

namespace Timesheets.DTOs.Requests
{
    public class TimesheetEntryRequest
    {
        [Required]
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