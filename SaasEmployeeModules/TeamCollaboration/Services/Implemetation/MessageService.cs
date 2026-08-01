using TeamCollaboration.DTOs.Requests;
using TeamCollaboration.DTOs.Responses;
using TeamCollaboration.Entities;
using TeamCollaboration.Repositories.Interfaces;
using TeamCollaboration.Services.Interfaces;

namespace TeamCollaboration.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationRepository _conversationRepository;

        public MessageService(
            IMessageRepository messageRepository,
            IConversationRepository conversationRepository)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
        }

        public async Task<List<MessageResponseDto>> GetMessagesAsync(long conversationId)
        {
            var conversation = await _conversationRepository.GetByIdAsync(conversationId);

            if (conversation == null)
            {
                throw new Exception("Conversation not found.");
            }

            var messages = await _messageRepository
                .GetConversationMessagesAsync(conversationId);

            return messages.Select(m => new MessageResponseDto
            {
                MessageId = m.MessageId,
                SenderId = m.SenderId,
                Content = m.Content,
                SentAt = m.SentAt
            }).ToList();
        }

        public async Task<MessageResponseDto> SendMessageAsync(
            long senderId,
            SendMessageRequestDto request)
        {
            var conversation = await _conversationRepository
                .GetByIdAsync(request.ConversationId);

            if (conversation == null)
            {
                throw new Exception("Conversation not found.");
            }

            var message = new Message
            {
                ConversationId = request.ConversationId,
                SenderId = senderId,
                Content = request.Content
            };

            await _messageRepository.AddAsync(message);

            return new MessageResponseDto
            {
                MessageId = message.MessageId,
                SenderId = message.SenderId,
                Content = message.Content,
                SentAt = message.SentAt
            };
        }
    }
}