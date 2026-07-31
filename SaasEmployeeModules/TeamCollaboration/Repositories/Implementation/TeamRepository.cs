using Microsoft.EntityFrameworkCore;
using TeamCollaboration.Data;
using TeamCollaboration.Entities;
using TeamCollaboration.Repositories.Interfaces;

namespace TeamCollaboration.Repositories.Implementation
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Team?> GetByIdAsync(long teamId)
        {
            return await _context.Teams
                .FirstOrDefaultAsync(t => t.TeamId == teamId);
        }

        public async Task<List<Team>> GetByCompanyIdAsync(long companyId)
        {
            return await _context.Teams
                .Where(t => t.CompanyId == companyId)
                .OrderByDescending(t => t.IsGeneral)
                .ThenBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<List<Team>> GetTeamsByCompanyAsync(long companyId)
        {
            return await _context.Teams
                .Where(t => t.CompanyId == companyId)
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<Team?> GetGeneralTeamAsync(long companyId)
        {
            return await _context.Teams
                .FirstOrDefaultAsync(t =>
                    t.CompanyId == companyId &&
                    t.IsGeneral);
        }

        public async Task<Team> AddAsync(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task<Team> UpdateAsync(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
            return team;
        }

        public async Task DeleteAsync(Team team)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }
    }
}