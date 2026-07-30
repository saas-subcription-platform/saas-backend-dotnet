using Microsoft.EntityFrameworkCore;
using TeamCollaboration.Entities;

namespace TeamCollaboration.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set;  }
    }
}
