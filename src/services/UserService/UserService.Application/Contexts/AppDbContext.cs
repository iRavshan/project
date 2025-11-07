using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Application.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Classroom> Classrooms => Set<Classroom>();
        public DbSet<ClassroomUser> ClassroomUsers => Set<ClassroomUser>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<Submission> Submissions => Set<Submission>();

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Classroom>()
                .HasIndex(c => c.Code)
                .IsUnique();

            // Classroom ↔ Teacher
            modelBuilder.Entity<Classroom>()
                .HasOne(c => c.Teacher)
                .WithMany(u => u.OwnedClassrooms)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Classroom ↔ Student many-to-many
            modelBuilder.Entity<ClassroomUser>()
                .HasKey(cu => new { cu.UserId, cu.ClassroomId });

            modelBuilder.Entity<ClassroomUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.JoinedClassrooms)
                .HasForeignKey(cu => cu.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClassroomUser>()
                .HasOne(cu => cu.Classroom)
                .WithMany(c => c.Students)
                .HasForeignKey(cu => cu.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Assignments
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Classroom)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.CreatedBy)
                .WithMany(u => u.CreatedAssignments)
                .HasForeignKey(a => a.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Submissions
            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Assignment)
                .WithMany(a => a.Submissions)
                .HasForeignKey(s => s.AssignmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Student)
                .WithMany(u => u.Submissions)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
