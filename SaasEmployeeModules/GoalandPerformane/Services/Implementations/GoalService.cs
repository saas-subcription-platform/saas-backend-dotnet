using GoalandPerformance.Entities;
using GoalandPerformance.Repositories.Interfaces;
using GoalandPerformance.Services.Interfaces;

namespace GoalandPerformance.Services.Implementations
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;

        public GoalService(IGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }

        public async Task<IEnumerable<Goal>> GetAllGoalsAsync()
        {
            return await _goalRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Goal>> GetGoalsByCompanyIdAsync(long companyId)
        {
            return await _goalRepository.GetByCompanyIdAsync(companyId);
        }

        public async Task<Goal?> GetGoalByIdAsync(long id)
        {
            return await _goalRepository.GetByIdAsync(id);
        }

        public async Task<Goal> CreateGoalAsync(Goal goal)
        {
            goal.Progress = 0;
            goal.Status = GoalStatus.NotStarted;

            await _goalRepository.AddAsync(goal);
            await _goalRepository.SaveChangesAsync();

            return goal;
        }

        public async Task<bool> UpdateGoalAsync(long id, Goal goal)
        {
            var existingGoal = await _goalRepository.GetByIdAsync(id);

            if (existingGoal == null)
                return false;

            existingGoal.Title = goal.Title;
            existingGoal.Objective = goal.Objective;
            existingGoal.Category = goal.Category;
            existingGoal.Priority = goal.Priority;
            existingGoal.Deadline = goal.Deadline;
            existingGoal.Progress = goal.Progress;
            existingGoal.Status = goal.Status;

            await _goalRepository.UpdateAsync(existingGoal);
            await _goalRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteGoalAsync(long id)
        {
            var goal = await _goalRepository.GetByIdAsync(id);

            if (goal == null)
                return false;

            await _goalRepository.DeleteAsync(goal);
            await _goalRepository.SaveChangesAsync();

            return true;
        }
    }
}