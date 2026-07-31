using TeamCollaboration.Entities;

namespace TeamCollaboration.Repositories.Interfaces
{
    public interface IConversationRepository
    {
        Task<Conversation?> GetByIdAsync(long conversationId);

        Task<Conversation?> GetDirectConversationAsync(long user1Id, long user2Id);

        Task<Conversation> AddAsync(Conversation conversation);
    }
}