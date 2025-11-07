using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class Assignment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required Guid ClassroomId { get; set; }
        public required Guid CreatedById { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public required Classroom Classroom { get; set; }
        public required User CreatedBy { get; set; }

        public ICollection<Submission> Submissions { get; set; } = [];
    }
}
