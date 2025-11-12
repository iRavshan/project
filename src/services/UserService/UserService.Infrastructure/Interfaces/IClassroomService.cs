using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Dtos.ClassroomDtos;

namespace UserService.Infrastructure.Interfaces
{
    public interface IClassroomService
    {
        Task<ClassroomDto> CreateClassroomAsync(Guid teacherId, ClassroomCreateDto dto);
        Task<ClassroomDto> GetClassroomByIdAsync(Guid classroomId);
        Task<List<ClassroomDto>> GetClassroomsByTeacherAsync(Guid teacherId);
        Task<ClassroomDto> UpdateClassroomAsync(Guid teacherId, Guid classroomId, ClassroomUpdateDto dto);
        Task DeleteClassroomAsync(Guid teacherId, Guid classroomId);
        Task EnrollStudentAsync(Guid teacherId, Guid classroomId, Guid studentId);
        Task RemoveStudentAsync(Guid teacherId, Guid classroomId, Guid studentId);
        Task<List<StudentDto>> GetStudentsInClassroomAsync(Guid teacherId, Guid classroomId);
    }
}
