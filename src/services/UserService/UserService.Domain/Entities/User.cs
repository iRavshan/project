using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public Role Role { get; set; }

        public ICollection<Classroom> OwnedClassrooms { get; set; } = [];
        public ICollection<ClassroomUser> JoinedClassrooms { get; set; } = [];
        public ICollection<Assignment> CreatedAssignments { get; set; } = [];
        public ICollection<Submission> Submissions { get; set; } = [];
    }
}
