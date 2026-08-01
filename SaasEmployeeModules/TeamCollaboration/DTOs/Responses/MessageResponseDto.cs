namespace TeamCollaboration.DTOs.Responses
{
    public class MessageResponseDto
    {
        public long MessageId { get; set; }

        public long SenderId { get; set; }

        public string Content { get; set; } = string.Empty;

        public DateTime SentAt { get; set; }
    }
}