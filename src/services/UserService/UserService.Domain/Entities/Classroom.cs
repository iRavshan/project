using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class Classroom
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string Code { get; set; }
        public string? Description { get; set; }
        public required Guid TeacherId { get; set; }
        public required User Teacher { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ClassroomUser> Students { get; set; } = [];
        public ICollection<Assignment> Assignments { get; set; } = [];
    }
}
