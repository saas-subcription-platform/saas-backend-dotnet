using TeamCollaboration.DTOs.Responses;

namespace TeamCollaboration.Services.Interfaces
{
    public interface ITeamMemberService
    {
        Task AddCompanyUsersToGeneralTeamAsync(
            long teamId,
            List<UserResponseDto> users);

        Task<List<UserResponseDto>> GetTeamMembersAsync(
            long teamId,
            string jwtToken);
    }
}