using TeamCollaboration.DTOs.Requests;
using TeamCollaboration.DTOs.Responses;
using TeamCollaboration.Entities;
using TeamCollaboration.Repositories.Interfaces;
using TeamCollaboration.Services.Interfaces;

namespace TeamCollaboration.Services.Implementation
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamMemberService _teamMemberService;

        public TeamService(
            ITeamRepository teamRepository,
            ITeamMemberService teamMemberService)
        {
            _teamRepository = teamRepository;
            _teamMemberService = teamMemberService;
        }

        public async Task<TeamResponseDto> GetOrCreateGeneralTeamAsync(
            long companyId,
            long userId,
            List<UserResponseDto> companyUsers)
        {
            var generalTeam = await _teamRepository.GetGeneralTeamAsync(companyId);

            if (generalTeam == null)
            {
                generalTeam = new Team
                {
                    Name = "General",
                    Description = "Default team for all company employees.",
                    IsGeneral = true,
                    CompanyId = companyId,
                    CreatedByUserId = userId,
                    CreatedAt = DateTime.UtcNow
                };

                generalTeam = await _teamRepository.AddAsync(generalTeam);
            }

            await _teamMemberService.AddCompanyUsersToGeneralTeamAsync(
                generalTeam.TeamId,
                companyUsers);

            return MapToResponse(generalTeam);
        }

        private TeamResponseDto MapToResponse(Team team)
        {
            return new TeamResponseDto
            {
                TeamId = team.TeamId,
                Name = team.Name,
                Description = team.Description,
                IsGeneral = team.IsGeneral,
                CompanyId = team.CompanyId,
                CreatedByUserId = team.CreatedByUserId,
                CreatedAt = team.CreatedAt,
                UpdatedAt = team.UpdatedAt
            };
        }

        public async Task<TeamResponseDto> CreateTeamAsync(
            CreateTeamRequestDto request,
            long companyId,
            long userId)
        {
            var team = new Team
            {
                Name = request.Name.Trim(),
                Description = string.IsNullOrWhiteSpace(request.Description)
                    ? null
                    : request.Description.Trim(),
                CompanyId = companyId,
                CreatedByUserId = userId,
                IsGeneral = false,
                CreatedAt = DateTime.UtcNow
            };

            team = await _teamRepository.AddAsync(team);

            // Add the creator as ADMIN
            await _teamMemberService.AddTeamMemberAsync(
                team.TeamId,
                userId,
                "ADMIN");

            // Add the selected members as MEMBER
            await _teamMemberService.AddMembersToTeamAsync(
                team.TeamId,
                request.MemberIds);

            return MapToResponse(team);
        }

        public async Task<List<TeamResponseDto>> GetTeamsByCompanyAsync(long companyId)
        {
            var teams = await _teamRepository.GetTeamsByCompanyAsync(companyId);

            return teams
                .Select(MapToResponse)
                .ToList();
        }

        public Task<TeamResponseDto?> GetByIdAsync(long teamId)
        {
            throw new NotImplementedException();
        }

        public Task<TeamResponseDto> UpdateTeamAsync(
            long teamId,
            UpdateTeamRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTeamAsync(long teamId)
        {
            throw new NotImplementedException();
        }
    }
}