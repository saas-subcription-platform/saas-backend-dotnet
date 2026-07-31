using System.ComponentModel.DataAnnotations;

namespace Timesheets.DTOs.Requests
{
    public class CreateTimesheetRequest
    {
        [Required]
        public DateOnly WeekStartDate { get; set; }

        [Required]
        public DateOnly WeekEndDate { get; set; }

        [Required]
        public List<TimesheetEntryRequest> Entries { get; set; } = new();
    }
}