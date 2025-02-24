using Microsoft.EntityFrameworkCore;
using Employee_Monitoring_System_API.Models;

namespace Employee_Monitoring_System_API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Branch and HeadUser
            modelBuilder.Entity<Branch>()
                .HasOne(b => b.HeadUser) // A Branch has one HeadUser
                .WithMany() // The HeadUser doesn't need a navigation property back to Branch
                .HasForeignKey(b => b.HeadUserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<_Task>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false); // Mark navigation as optional

            modelBuilder.Entity<_Task>()
            .HasOne(t => t.AssignedUser)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.AssignedTo)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false); // Mark navigation as optional

            modelBuilder.Entity<Screenshot>()
                .HasOne(s => s.User)
                .WithMany(u => u.Screenshots)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
            
            modelBuilder.Entity<ActivityLog>()
                .HasOne(a => a.User)
                .WithMany(u => u.ActivityLogs)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(l => l.User)
                .WithMany(u => u.LeaveRequests)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<_Task> Tasks { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Screenshot> Screenshots { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<AppSettings> AppSettings { get; set; }
    }
}
