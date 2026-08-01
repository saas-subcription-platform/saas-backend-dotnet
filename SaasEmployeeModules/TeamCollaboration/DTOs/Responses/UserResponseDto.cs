namespace TeamCollaboration.DTOs.Responses
{
    public class UserResponseDto
    {
        public long UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public long CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;
    }
}
