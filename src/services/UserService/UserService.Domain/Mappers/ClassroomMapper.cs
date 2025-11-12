using UserService.Domain.Dtos.ClassroomDtos;
using UserService.Domain.Entities;

namespace UserService.Domain.Mappers;

public static class ClassroomMapper
{
    public static ClassroomDto ToDto(this Classroom classroom)
    {
        return new ClassroomDto(
            classroom.Id,
            classroom.Name,
            classroom.Code,
            classroom.Description,
            classroom.TeacherId,
            classroom.Teacher.UserName,
            classroom.CreatedAt,
            classroom.Students.Select(s => new StudentDto(
                s.UserId,
                s.User.UserName,
                s.User.FirstName,
                s.User.LastName,
                s.User.Email,
                s.User.Phone)).ToList()
        );
    }

    public static void UpdateFromDto(this Classroom classroom, ClassroomUpdateDto dto)
    {
        classroom.Name = dto.Name;
        classroom.Description = dto.Description;
    }
}
