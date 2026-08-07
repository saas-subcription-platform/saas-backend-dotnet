namespace GoalandPerformance.DTOs.Response
{
    public class GoalStatisticsDto
    {
        public int ActiveGoals { get; set; }

        public int CompletedGoals { get; set; }

        public int OverdueGoals { get; set; }

        public int AverageProgress { get; set; }
    }
}