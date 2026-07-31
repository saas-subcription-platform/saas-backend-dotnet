using Timesheets.Entities;

namespace Timesheets.Repositories.Interfaces
{
    public interface ITimesheetRepository
    {
        Task<IEnumerable<Timesheet>> GetAllByEmployeeIdAsync(int employeeId);

        Task<Timesheet?> GetByIdAsync(int id);

        Task AddAsync(Timesheet timesheet);

        void Update(Timesheet timesheet);

        void Delete(Timesheet timesheet);

        Task SaveChangesAsync();
    }
}