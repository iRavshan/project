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
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; }
        public string? Phone { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public Role Role { get; set; }
        

        public User(
            Guid id,
            Role role,
            string username,
            string passwordHash,
            string firstName,
            string lastName,
            string email,
            string phone
        )
        {
            Id = id;
            Role = role;
            Username = username;
            PasswordHash = passwordHash;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

        public User(
            Guid id,
            Role role,
            string username,
            string email,
            string passwordHash)
        {
            Id = id;
            Role = role;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }

        public ICollection<Classroom> OwnedClassrooms { get; set; } = [];
        public ICollection<ClassroomUser> JoinedClassrooms { get; set; } = [];
        public ICollection<Assignment> CreatedAssignments { get; set; } = [];
        public ICollection<Submission> Submissions { get; set; } = [];
    }
}
