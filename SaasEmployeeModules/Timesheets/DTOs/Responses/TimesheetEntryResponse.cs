namespace Timesheets.DTOs.Responses
{
    public class TimesheetEntryResponse
    {
        public int Id { get; set; }

        public string Day { get; set; } = string.Empty;

        public string ProjectName { get; set; } = string.Empty;

        public string Task { get; set; } = string.Empty;

        public decimal Hours { get; set; }

        public string? Description { get; set; }
    }
}