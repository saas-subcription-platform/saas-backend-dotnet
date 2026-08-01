using TeamCollaboration.Entities;

namespace TeamCollaboration.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetConversationMessagesAsync(long conversationId);

        Task<Message> AddAsync(Message message);
    }
}