using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCollaboration.Entities
{
    [Table("conversation_participants")]
    public class ConversationParticipant
    {
        [Key]
        [Column("conversation_participant_id")]
        public long ConversationParticipantId { get; set; }

        [Required]
        [Column("conversation_id")]
        public long ConversationId { get; set; }

        // User ID from Spring Boot
        [Required]
        [Column("user_id")]
        public long UserId { get; set; }

        [Column("joined_at")]
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property
        [ForeignKey(nameof(ConversationId))]
        public Conversation Conversation { get; set; } = null!;
    }
}