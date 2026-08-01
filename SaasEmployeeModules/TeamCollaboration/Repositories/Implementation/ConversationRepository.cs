using Microsoft.EntityFrameworkCore;
using TeamCollaboration.Data;
using TeamCollaboration.Entities;
using TeamCollaboration.Repositories.Interfaces;

namespace TeamCollaboration.Repositories.Implementation
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _context;

        public ConversationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation?> GetByIdAsync(long conversationId)
        {
            return await _context.Conversations
                .FirstOrDefaultAsync(c => c.ConversationId == conversationId);
        }

        public async Task<Conversation?> GetDirectConversationAsync(long user1Id, long user2Id)
        {
            var conversation = await _context.Conversations
                .Include(c => c.Participants)
                .FirstOrDefaultAsync(c =>
                    c.Type == "DIRECT" &&
                    c.Participants.Any(p => p.UserId == user1Id) &&
                    c.Participants.Any(p => p.UserId == user2Id));

            return conversation;
        }

        public async Task<Conversation> AddAsync(Conversation conversation)
        {
            await _context.Conversations.AddAsync(conversation);
            await _context.SaveChangesAsync();
            return conversation;
        }
    }
}