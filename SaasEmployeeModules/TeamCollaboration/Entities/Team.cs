using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCollaboration.Entities
{
    [Table("teams")]
    public class Team
    {
        [Key]
        [Column("team_id")]
        public long TeamId { get; set; }

        [Required]
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [Column("is_general")]
        public bool IsGeneral { get; set; } = false;

        // Company ID from Spring Boot
        [Required]
        [Column("company_id")]
        public long CompanyId { get; set; }

        // User ID from Spring Boot
        [Required]
        [Column("created_by_user_id")]
        public long CreatedByUserId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    }
}