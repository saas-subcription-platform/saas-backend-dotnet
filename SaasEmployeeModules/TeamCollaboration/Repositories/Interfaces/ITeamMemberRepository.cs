using TeamCollaboration.Entities;

namespace TeamCollaboration.Repositories.Interfaces
{
    public interface ITeamMemberRepository
    {
        Task<TeamMember?> GetTeamMemberAsync(long teamId, long userId);

        Task<List<TeamMember>> GetTeamMembersAsync(long teamId);

        Task AddTeamMemberAsync(TeamMember teamMember);

        Task SaveChangesAsync();
    }
}