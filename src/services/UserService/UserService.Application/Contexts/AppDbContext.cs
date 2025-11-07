using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Application.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
    }
}
