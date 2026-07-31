using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamCollaboration.Entities
{
    [Table("conversations")]
    public class Conversation
    {
        [Key]
        [Column("conversation_id")]
        public long ConversationId { get; set; }

        [Required]
        [Column("type")]
        [MaxLength(20)]
        public string Type { get; set; } = "DIRECT";


        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties
        public ICollection<ConversationParticipant> Participants { get; set; }
            = new List<ConversationParticipant>();

        public ICollection<Message> Messages { get; set; }
            = new List<Message>();
    }
}