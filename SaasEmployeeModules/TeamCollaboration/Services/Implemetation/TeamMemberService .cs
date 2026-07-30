using TeamCollaboration.DTOs.Responses;
using TeamCollaboration.Entities;
using TeamCollaboration.Repositories.Interfaces;
using TeamCollaboration.Services.Interfaces;

namespace TeamCollaboration.Services.Implementation
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly ISpringBootUserService _springBootUserService;


        public TeamMemberService(ITeamMemberRepository teamMemberRepository, ISpringBootUserService springBootUserService)
        {
            _teamMemberRepository = teamMemberRepository;
            _springBootUserService = springBootUserService;

        }

        public async Task AddCompanyUsersToGeneralTeamAsync(
            long teamId,
            List<UserResponseDto> users)
        {
            foreach (var user in users)
            {
                var existingMember =
                    await _teamMemberRepository.GetTeamMemberAsync(
                        teamId,
                        user.UserId);

                if (existingMember != null)
                    continue;

                var teamMember = new TeamMember
                {
                    TeamId = teamId,
                    UserId = user.UserId,
                    Role = "MEMBER"
                };

                await _teamMemberRepository.AddTeamMemberAsync(teamMember);
            }

            await _teamMemberRepository.SaveChangesAsync();
        }

        public async Task<List<UserResponseDto>> GetTeamMembersAsync(
    long teamId,
    string jwtToken)
        {
            // Get members from database
            var teamMembers = await _teamMemberRepository.GetTeamMembersAsync(teamId);

            // Get all company users from Spring Boot
            var companyUsers =
                await _springBootUserService.GetCompanyUsersAsync(jwtToken);

            // Get member ids
            var memberIds = teamMembers
                .Select(tm => tm.UserId)
                .ToHashSet();

            // Return matching users
            return companyUsers
                .Where(user => memberIds.Contains(user.UserId))
                .ToList();
        }
    }
}