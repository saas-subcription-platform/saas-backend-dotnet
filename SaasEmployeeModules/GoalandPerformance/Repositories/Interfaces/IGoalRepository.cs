using GoalandPerformance.Entities;

namespace GoalandPerformance.Repositories.Interfaces
{
    public interface IGoalRepository
    {
        Task<IEnumerable<Goal>> GetAllAsync();

        Task<Goal?> GetByIdAsync(long id);

        Task<IEnumerable<Goal>> GetByCompanyIdAsync(long companyId);

        Task AddAsync(Goal goal);

        Task UpdateAsync(Goal goal);

        Task DeleteAsync(Goal goal);

        Task SaveChangesAsync();
    }
}