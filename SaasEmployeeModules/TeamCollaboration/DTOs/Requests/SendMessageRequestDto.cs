using System.ComponentModel.DataAnnotations;

namespace TeamCollaboration.DTOs.Requests
{
    public class SendMessageRequestDto
    {
        [Required]
        public long ConversationId { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; } = string.Empty;
    }
}