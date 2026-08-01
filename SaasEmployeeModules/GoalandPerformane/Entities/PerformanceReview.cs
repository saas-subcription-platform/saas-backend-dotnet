using GoalandPerformance.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalandPerformance.Entities
{
    [Table("performance_reviews")]
    public class PerformanceReview : BaseEntity
    {
        [Column("goal_id")]
        public long GoalId { get; set; }

        [Column("rating")]
        public PerformanceRating Rating { get; set; }

        [Column("feedback")]
        [MaxLength(1000)]
        public string? Feedback { get; set; }

        [Column("review_date")]
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    }
}