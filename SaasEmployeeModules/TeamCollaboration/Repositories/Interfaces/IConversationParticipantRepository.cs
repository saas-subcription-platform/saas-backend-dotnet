using TeamCollaboration.Entities;

namespace TeamCollaboration.Repositories.Interfaces
{
    public interface IConversationParticipantRepository
    {
        Task<ConversationParticipant> AddAsync(ConversationParticipant participant);

        Task AddRangeAsync(List<ConversationParticipant> participants);

        Task<List<ConversationParticipant>> GetParticipantsAsync(long conversationId);
    }
}