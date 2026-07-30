using Microsoft.EntityFrameworkCore;
using Timesheets.Data;
using Timesheets.Entities;
using Timesheets.Repositories.Interfaces;

namespace Timesheets.Repositories.Implementations
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly ApplicationDbContext _context;

        public TimesheetRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Timesheet>> GetAllByEmployeeIdAsync(int employeeId)
        {
            return await _context.Timesheets
                .Include(t => t.Entries)
                .Where(t => t.EmployeeId == employeeId)
                .OrderByDescending(t => t.WeekStartDate)
                .ToListAsync();
        }

        public async Task<Timesheet?> GetByIdAsync(int id)
        {
            return await _context.Timesheets
                .Include(t => t.Entries)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Timesheet timesheet)
        {
            await _context.Timesheets.AddAsync(timesheet);
        }

        public void Update(Timesheet timesheet)
        {
            _context.Timesheets.Update(timesheet);
        }

        public void Delete(Timesheet timesheet)
        {
            _context.Timesheets.Remove(timesheet);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}