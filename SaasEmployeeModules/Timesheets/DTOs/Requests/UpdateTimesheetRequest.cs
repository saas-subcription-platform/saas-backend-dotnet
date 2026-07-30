using System.ComponentModel.DataAnnotations;

namespace Timesheets.DTOs.Requests
{
    public class UpdateTimesheetRequest
    {
        [Required]
        public DateOnly WeekStartDate { get; set; }

        [Required]
        public DateOnly WeekEndDate { get; set; }

        [Required]
        public List<TimesheetEntryRequest> Entries { get; set; } = new();
    }
}