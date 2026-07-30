using GoalandPerformance.Entities;

namespace GoalandPerformance.Services.Interfaces
{
    public interface IGoalService
    {
        Task<IEnumerable<Goal>> GetAllGoalsAsync();

        Task<IEnumerable<Goal>> GetGoalsByCompanyIdAsync(long companyId);

        Task<Goal?> GetGoalByIdAsync(long id);

        Task<Goal> CreateGoalAsync(Goal goal);

        Task<bool> UpdateGoalAsync(long id, Goal goal);

        Task<bool> DeleteGoalAsync(long id);
    }
}