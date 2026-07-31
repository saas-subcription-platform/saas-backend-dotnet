using System.ComponentModel.DataAnnotations;

namespace Timesheets.DTOs.Requests
{
    public class TimesheetRequest
    {
        [Required]
        public DateOnly WeekStartDate { get; set; }

        [Required]
        public DateOnly WeekEndDate { get; set; }

        public List<TimesheetEntryRequest> Entries { get; set; } = new();
    }
}