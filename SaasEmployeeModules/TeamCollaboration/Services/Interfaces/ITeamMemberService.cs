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
        Task AddTeamMemberAsync(
            long teamId,
            long userId,
            string role);
        Task AddMembersToTeamAsync(
            long teamId,
            List<long> memberIds);
    }
}