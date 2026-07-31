using TeamCollaboration.DTOs.Requests;
using TeamCollaboration.DTOs.Responses;

namespace TeamCollaboration.Services.Interfaces
{
    public interface ITeamService
    {
        Task<TeamResponseDto> GetOrCreateGeneralTeamAsync(
            long companyId,
            long userId,
            List<UserResponseDto> companyUsers);

        Task<TeamResponseDto> CreateTeamAsync(
            CreateTeamRequestDto request,
            long companyId,
            long userId);

        Task<List<TeamResponseDto>> GetTeamsByCompanyAsync(long companyId);

        Task<TeamResponseDto?> GetByIdAsync(long teamId);

        Task<TeamResponseDto> UpdateTeamAsync(
            long teamId,
            UpdateTeamRequestDto request);

        Task DeleteTeamAsync(long teamId);


    }
}