using Timesheets.DTOs.Requests;
using Timesheets.DTOs.Responses;

namespace Timesheets.Services.Interfaces
{
    public interface ITimesheetService
    {
        Task<IEnumerable<TimesheetResponse>> GetAllByEmployeeIdAsync(int employeeId);

        Task<TimesheetResponse?> GetByIdAsync(int id, int employeeId);

        Task<TimesheetResponse> CreateAsync(TimesheetRequest request, int employeeId);

        Task<TimesheetResponse?> UpdateAsync(int id, TimesheetRequest request, int employeeId);

        Task<TimesheetResponse?> SubmitAsync(int id, int employeeId);
    }
}