using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UserService.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Phone { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public Guid? OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public ICollection<Classroom> OwnedClassrooms { get; set; } = [];
        public ICollection<ClassroomUser> JoinedClassrooms { get; set; } = [];
        public ICollection<Assignment> CreatedAssignments { get; set; } = [];
        public ICollection<Submission> Submissions { get; set; } = [];
    }
}
