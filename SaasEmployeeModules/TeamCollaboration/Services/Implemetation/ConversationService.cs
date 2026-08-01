using TeamCollaboration.DTOs.Responses;
using TeamCollaboration.Entities;
using TeamCollaboration.Repositories.Interfaces;
using TeamCollaboration.Services.Interfaces;

namespace TeamCollaboration.Services.Implementation
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IConversationParticipantRepository _participantRepository;

        public ConversationService(
            IConversationRepository conversationRepository,
            IConversationParticipantRepository participantRepository)
        {
            _conversationRepository = conversationRepository;
            _participantRepository = participantRepository;
        }

        public async Task<ConversationResponseDto> GetOrCreateDirectConversationAsync(
            long currentUserId,
            long otherUserId)
        {
            // Check if a direct conversation already exists
            var conversation = await _conversationRepository
                .GetDirectConversationAsync(currentUserId, otherUserId);

            // If not found, create a new conversation
            if (conversation == null)
            {
                conversation = new Conversation
                {
                    Type = "DIRECT"
                };

                await _conversationRepository.AddAsync(conversation);

                // Add both users as participants
                await _participantRepository.AddRangeAsync(
                    new List<ConversationParticipant>
                    {
                        new ConversationParticipant
                        {
                            ConversationId = conversation.ConversationId,
                            UserId = currentUserId
                        },
                        new ConversationParticipant
                        {
                            ConversationId = conversation.ConversationId,
                            UserId = otherUserId
                        }
                    });
            }

            return new ConversationResponseDto
            {
                ConversationId = conversation.ConversationId,
                Type = conversation.Type
            };
        }
    }
}