using TeamCollaboration.DTOs.Responses;

namespace TeamCollaboration.Services.Interfaces
{
    public interface ISpringBootUserService
    {
        Task<List<UserResponseDto>> GetCompanyUsersAsync(string jwtToken);
        Task<UserResponseDto> GetCurrentUserAsync(string jwtToken);
    }
}
