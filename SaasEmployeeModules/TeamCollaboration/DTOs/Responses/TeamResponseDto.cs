namespace TeamCollaboration.DTOs.Responses
{
    public class TeamResponseDto
    {
        public long TeamId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsGeneral { get; set; }

        public long CompanyId { get; set; }

        public long CreatedByUserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}