using GoalandPerformance.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalandPerformance.Entities
{
    [Table("goals")]
    public class Goal : BaseEntity
    {
        [Column("title")]
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Column("objective")]
        [MaxLength(1000)]
        public string? Objective { get; set; }

        [Column("category")]
        [Required]
        [MaxLength(100)]
        public string Category { get; set; } = string.Empty;

        [Column("priority")]
        public GoalPriority Priority { get; set; }

        [Column("deadline")]
        public DateTime Deadline { get; set; }

        // References Spring Boot User
        [Column("user_id")]
        public long UserId { get; set; }

        // References Spring Boot Company
        [Column("company_id")]
        public long CompanyId { get; set; }

        // Default values after creation
        [Column("status")]
        public GoalStatus Status { get; set; } = GoalStatus.NotStarted;

        [Column("progress")]
        public int Progress { get; set; } = 0;
    }
}