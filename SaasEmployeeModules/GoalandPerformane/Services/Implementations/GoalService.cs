using GoalandPerformance.Entities;
using GoalandPerformance.Repositories.Interfaces;
using GoalandPerformance.Services.Interfaces;
using GoalandPerformance.DTOs.Response;
using System.Linq;

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

            goal.Deadline = DateTime.SpecifyKind(goal.Deadline, DateTimeKind.Utc);

            goal.CreatedAt = DateTime.UtcNow;
            goal.UpdatedAt = DateTime.UtcNow;

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
            existingGoal.Deadline = DateTime.SpecifyKind(goal.Deadline, DateTimeKind.Utc);
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

        public async Task<GoalStatisticsDto> GetGoalStatisticsAsync(long companyId)
        {
            var goals = (await _goalRepository.GetByCompanyIdAsync(companyId)).ToList();

            var completedGoals = goals.Count(g => g.Status == GoalStatus.Completed);

            var overdueGoals = goals.Count(g =>
                g.Status != GoalStatus.Completed &&
                g.Deadline < DateTime.UtcNow);

            var activeGoals = goals.Count(g =>
                g.Status != GoalStatus.Completed &&
                g.Deadline >= DateTime.UtcNow);

            var averageProgress = goals.Any()
                ? (int)Math.Round(goals.Average(g => g.Progress))
                : 0;

            return new GoalStatisticsDto
            {
                ActiveGoals = activeGoals,
                CompletedGoals = completedGoals,
                OverdueGoals = overdueGoals,
                AverageProgress = averageProgress
            };
        }
    }
}