using TeamCollaboration.DTOs.Responses;

namespace TeamCollaboration.Services.Interfaces
{
    public interface IConversationService
    {
        Task<ConversationResponseDto> GetOrCreateDirectConversationAsync(
            long currentUserId,
            long otherUserId);
    }
}