using GoalandPerformance.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalandPerformance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Goal> Goals { get; set; }

        public DbSet<PerformanceReview> PerformanceReviews { get; set; }
    }
}