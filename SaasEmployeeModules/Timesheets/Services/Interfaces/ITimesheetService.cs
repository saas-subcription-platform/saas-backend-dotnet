using Timesheets.DTOs.Responses;

namespace Timesheets.Services.Interfaces
{
    public interface ITimesheetService
    {
        Task<IEnumerable<TimesheetResponse>> GetAllByEmployeeIdAsync(int employeeId);
    }
}