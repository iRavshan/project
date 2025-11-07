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
        [Key]
        public Guid Id { get; set; }
        public Role Role { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

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
    }
}
