using Microsoft.EntityFrameworkCore;
using Timesheets.Entities;

namespace Timesheets.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Timesheet> Timesheets { get; set; }

        public DbSet<TimesheetEntry> TimesheetEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Timesheet>()
                .HasMany(t => t.Entries)
                .WithOne(e => e.Timesheet)
                .HasForeignKey(e => e.TimesheetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Timesheet>()
                .Property(t => t.Status)
                .HasConversion<string>()
                .HasMaxLength(20);
        }
    }
}