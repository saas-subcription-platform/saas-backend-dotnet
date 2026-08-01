namespace Timesheets.DTOs.Responses
{
    public class TimesheetResponse
    {
        public int Id { get; set; }

        public DateOnly WeekStartDate { get; set; }

        public DateOnly WeekEndDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal TotalHours { get; set; }

        public DateTime? SubmittedOn { get; set; }

        public List<TimesheetEntryResponse> Entries { get; set; } = new();
    }
}