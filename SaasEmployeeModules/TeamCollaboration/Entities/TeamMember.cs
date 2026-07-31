using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCollaboration.Entities
{
    [Table("team_members")]
    public class TeamMember
    {
        [Key]
        [Column("team_member_id")]
        public long TeamMemberId { get; set; }

        [Required]
        [Column("team_id")]
        public long TeamId { get; set; }

        // User ID from Spring Boot
        [Required]
        [Column("user_id")]
        public long UserId { get; set; }

        [Required]
        [Column("role")]
        [MaxLength(20)]
        public string Role { get; set; } = "MEMBER";

        [Column("joined_at")]
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; } = null!;
    }
}