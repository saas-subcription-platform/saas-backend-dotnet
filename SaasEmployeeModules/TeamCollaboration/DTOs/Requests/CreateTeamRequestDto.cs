using System.ComponentModel.DataAnnotations;

namespace TeamCollaboration.DTOs.Requests
{
    public class CreateTeamRequestDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }
    }
}