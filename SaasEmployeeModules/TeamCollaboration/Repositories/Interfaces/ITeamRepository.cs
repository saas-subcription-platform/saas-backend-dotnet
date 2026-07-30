using TeamCollaboration.Entities;

namespace TeamCollaboration.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<Team?> GetByIdAsync(long teamId);

        Task<List<Team>> GetByCompanyIdAsync(long companyId);

        Task<Team?> GetGeneralTeamAsync(long companyId);

        Task<Team> AddAsync(Team team);

        Task<Team> UpdateAsync(Team team);

        Task DeleteAsync(Team team);

        Task<List<Team>> GetTeamsByCompanyAsync(long companyId);
    }
}
