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
        private readonly IConversationRepository _conversationRepository;
        private readonly IConversationParticipantRepository _conversationParticipantRepository;

        public TeamService(
            ITeamRepository teamRepository,
            ITeamMemberService teamMemberService,
            IConversationRepository conversationRepository,
            IConversationParticipantRepository conversationParticipantRepository)
        {
            _teamRepository = teamRepository;
            _teamMemberService = teamMemberService;
            _conversationRepository = conversationRepository;
            _conversationParticipantRepository = conversationParticipantRepository;
        }

        public async Task<TeamResponseDto> GetOrCreateGeneralTeamAsync(
            long companyId,
            long userId,
            List<UserResponseDto> companyUsers)
        {
            var generalTeam = await _teamRepository.GetGeneralTeamAsync(companyId);

            if (generalTeam == null)
            {
                var conversation = await _conversationRepository.AddAsync(
                    new Conversation
                    {
                        Type = "GROUP"
                    });
                generalTeam = new Team
                {
                    Name = "General",
                    Description = "Default team for all company employees.",
                    IsGeneral = true,
                    CompanyId = companyId,
                    CreatedByUserId = userId,
                    ConversationId = conversation.ConversationId,
                    CreatedAt = DateTime.UtcNow
                };

                generalTeam = await _teamRepository.AddAsync(generalTeam);
                var participants = companyUsers
                        .Select(user => new ConversationParticipant
                        {
                            ConversationId = conversation.ConversationId,
                            UserId = user.UserId,
                            JoinedAt = DateTime.UtcNow
                        })
                        .ToList();

                await _conversationParticipantRepository.AddRangeAsync(participants);
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
                ConversationId = team.ConversationId,
                CreatedAt = team.CreatedAt,
                UpdatedAt = team.UpdatedAt
            };
        }

        public async Task<TeamResponseDto> CreateTeamAsync(
            CreateTeamRequestDto request,
            long companyId,
            long userId)
        {
            // Step 1: Create GROUP conversation
            var conversation = new Conversation
            {
                Type = "GROUP",
                CreatedAt = DateTime.UtcNow
            };

            conversation = await _conversationRepository.AddAsync(conversation);

            // Step 2: Create Team
            var team = new Team
            {
                Name = request.Name.Trim(),
                Description = string.IsNullOrWhiteSpace(request.Description)
                    ? null
                    : request.Description.Trim(),
                CompanyId = companyId,
                CreatedByUserId = userId,
                IsGeneral = false,
                ConversationId = conversation.ConversationId,
                CreatedAt = DateTime.UtcNow
            };

            team = await _teamRepository.AddAsync(team);

            // Step 3: Add creator as ADMIN
            await _teamMemberService.AddTeamMemberAsync(
                team.TeamId,
                userId,
                "ADMIN");

            // Step 4: Add selected users to TeamMembers
            await _teamMemberService.AddMembersToTeamAsync(
                team.TeamId,
                request.MemberIds);

            // Step 5: Create Conversation Participants
            var participants = new List<ConversationParticipant>();

            // Creator
            participants.Add(new ConversationParticipant
            {
                ConversationId = conversation.ConversationId,
                UserId = userId,
                JoinedAt = DateTime.UtcNow
            });

            // Selected Members
            foreach (var memberId in request.MemberIds.Distinct())
            {
                if (memberId == userId)
                    continue;

                participants.Add(new ConversationParticipant
                {
                    ConversationId = conversation.ConversationId,
                    UserId = memberId,
                    JoinedAt = DateTime.UtcNow
                });
            }

            await _conversationParticipantRepository.AddRangeAsync(participants);

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