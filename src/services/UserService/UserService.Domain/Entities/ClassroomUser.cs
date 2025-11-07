using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entities
{
    public class ClassroomUser
    {
        public Guid UserId { get; set; }
        public Guid ClassroomId { get; set; }

        public required User User { get; set; }
        public required Classroom Classroom { get; set; }
    }
}
