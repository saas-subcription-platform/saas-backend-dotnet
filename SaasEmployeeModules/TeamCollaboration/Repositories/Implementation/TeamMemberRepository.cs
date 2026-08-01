using Microsoft.EntityFrameworkCore;
using TeamCollaboration.Data;
using TeamCollaboration.Entities;
using TeamCollaboration.Repositories.Interfaces;

namespace TeamCollaboration.Repositories.Implementation
{
    public class TeamMemberRepository : ITeamMemberRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamMemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TeamMember?> GetTeamMemberAsync(long teamId, long userId)
        {
            return await _context.TeamMembers
                .FirstOrDefaultAsync(tm =>
                    tm.TeamId == teamId &&
                    tm.UserId == userId);
        }

        public async Task<List<TeamMember>> GetTeamMembersAsync(long teamId)
        {
            return await _context.TeamMembers
                .Where(tm => tm.TeamId == teamId)
                .ToListAsync();
        }

        public async Task AddTeamMemberAsync(TeamMember teamMember)
        {
            await _context.TeamMembers.AddAsync(teamMember);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}