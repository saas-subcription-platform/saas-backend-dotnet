using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCollaboration.Entities
{
    [Table("messages")]
    public class Message
    {
        [Key]
        [Column("message_id")]
        public long MessageId { get; set; }

        [Required]
        [Column("conversation_id")]
        public long ConversationId { get; set; }

        // User ID from Spring Boot
        [Required]
        [Column("sender_id")]
        public long SenderId { get; set; }

        [Required]
        [Column("content")]
        [MaxLength(2000)]
        public string Content { get; set; } = string.Empty;

        [Column("sent_at")]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        [Column("edited_at")]
        public DateTime? EditedAt { get; set; }

        // Navigation Property
        [ForeignKey(nameof(ConversationId))]
        public Conversation Conversation { get; set; } = null!;
    }
}