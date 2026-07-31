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

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<ConversationParticipant> ConversationParticipants { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}
