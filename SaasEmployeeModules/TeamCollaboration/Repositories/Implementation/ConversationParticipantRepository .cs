using Microsoft.EntityFrameworkCore;
using TeamCollaboration.Data;
using TeamCollaboration.Entities;
using TeamCollaboration.Repositories.Interfaces;

namespace TeamCollaboration.Repositories.Implementation
{
    public class ConversationParticipantRepository : IConversationParticipantRepository
    {
        private readonly ApplicationDbContext _context;

        public ConversationParticipantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ConversationParticipant> AddAsync(ConversationParticipant participant)
        {
            await _context.ConversationParticipants.AddAsync(participant);
            await _context.SaveChangesAsync();

            return participant;
        }

        public async Task AddRangeAsync(List<ConversationParticipant> participants)
        {
            await _context.ConversationParticipants.AddRangeAsync(participants);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ConversationParticipant>> GetParticipantsAsync(long conversationId)
        {
            return await _context.ConversationParticipants
                .Where(p => p.ConversationId == conversationId)
                .OrderBy(p => p.UserId)
                .ToListAsync();
        }
    }
}