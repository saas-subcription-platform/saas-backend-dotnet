using TeamCollaboration.DTOs.Requests;
using TeamCollaboration.DTOs.Responses;

namespace TeamCollaboration.Services.Interfaces
{
    public interface IMessageService
    {
        Task<List<MessageResponseDto>> GetMessagesAsync(long conversationId);

        Task<MessageResponseDto> SendMessageAsync(
            long senderId,
            SendMessageRequestDto request);
    }
}