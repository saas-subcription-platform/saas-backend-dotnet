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
        private readonly IConversationParticipantRepository _conversationParticipantRepository;
        private readonly ITeamRepository _teamRepository;

        public TeamMemberService(
            ITeamMemberRepository teamMemberRepository,
            ISpringBootUserService springBootUserService,
            IConversationParticipantRepository conversationParticipantRepository,
            ITeamRepository teamRepository)
        {
            _teamMemberRepository = teamMemberRepository;
            _springBootUserService = springBootUserService;
            _conversationParticipantRepository = conversationParticipantRepository;
            _teamRepository = teamRepository;
        }

        public async Task AddCompanyUsersToGeneralTeamAsync(
            long teamId,
            List<UserResponseDto> users)
        {
            // Get the General Team
            var team = await _teamRepository.GetByIdAsync(teamId);

            if (team == null)
                throw new Exception("Team not found.");

            // Load existing conversation participants once
            var participants =
                await _conversationParticipantRepository.GetParticipantsAsync(
                    team.ConversationId);

            var participantIds = participants
                .Select(p => p.UserId)
                .ToHashSet();

            foreach (var user in users)
            {
                // ---------------- TEAM MEMBER ----------------

                var existingMember =
                    await _teamMemberRepository.GetTeamMemberAsync(
                        teamId,
                        user.UserId);

                if (existingMember == null)
                {
                    var teamMember = new TeamMember
                    {
                        TeamId = teamId,
                        UserId = user.UserId,
                        Role = "MEMBER"
                    };

                    await _teamMemberRepository.AddTeamMemberAsync(teamMember);
                }

                // -------- CONVERSATION PARTICIPANT --------

                if (!participantIds.Contains(user.UserId))
                {
                    await _conversationParticipantRepository.AddAsync(
                        new ConversationParticipant
                        {
                            ConversationId = team.ConversationId,
                            UserId = user.UserId,
                            JoinedAt = DateTime.UtcNow
                        });

                    // Keep the HashSet updated to avoid duplicates
                    participantIds.Add(user.UserId);
                }
            }

            await _teamMemberRepository.SaveChangesAsync();
        }

        public async Task AddMembersToTeamAsync(
            long teamId,
            List<long> memberIds)
        {
            foreach (var userId in memberIds)
            {
                var existingMember =
                    await _teamMemberRepository.GetTeamMemberAsync(
                        teamId,
                        userId);

                if (existingMember != null)
                    continue;

                var member = new TeamMember
                {
                    TeamId = teamId,
                    UserId = userId,
                    Role = "MEMBER"
                };

                await _teamMemberRepository.AddTeamMemberAsync(member);
            }

            await _teamMemberRepository.SaveChangesAsync();
        }

        public async Task AddTeamMemberAsync(
            long teamId,
            long userId,
            string role)
        {
            var existingMember =
                await _teamMemberRepository.GetTeamMemberAsync(
                    teamId,
                    userId);

            if (existingMember != null)
                return;

            var teamMember = new TeamMember
            {
                TeamId = teamId,
                UserId = userId,
                Role = role
            };

            await _teamMemberRepository.AddTeamMemberAsync(teamMember);
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