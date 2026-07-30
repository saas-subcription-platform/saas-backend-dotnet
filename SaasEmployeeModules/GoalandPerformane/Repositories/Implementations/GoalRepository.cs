using GoalandPerformance.Data;
using GoalandPerformance.Entities;
using GoalandPerformance.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoalandPerformance.Repositories.Implementations
{
    public class GoalRepository : IGoalRepository
    {
        private readonly ApplicationDbContext _context;

        public GoalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Goal>> GetAllAsync()
        {
            return await _context.Goals.ToListAsync();
        }

        public async Task<Goal?> GetByIdAsync(long id)
        {
            return await _context.Goals.FindAsync(id);
        }

        public async Task<IEnumerable<Goal>> GetByCompanyIdAsync(long companyId)
        {
            return await _context.Goals
                .Where(g => g.CompanyId == companyId)
                .ToListAsync();
        }

        public async Task AddAsync(Goal goal)
        {
            await _context.Goals.AddAsync(goal);
        }

        public Task UpdateAsync(Goal goal)
        {
            _context.Goals.Update(goal);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Goal goal)
        {
            _context.Goals.Remove(goal);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}