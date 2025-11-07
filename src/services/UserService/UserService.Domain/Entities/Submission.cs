using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class Submission
    {
        public int Id { get; set; }
        public required Guid AssignmentId { get; set; }
        public required Guid StudentId { get; set; }
        public string? Content { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public decimal? Grade { get; set; }
        public string? Feedback { get; set; }

        public required Assignment Assignment { get; set; }
        public required User Student { get; set; }
    }
}
