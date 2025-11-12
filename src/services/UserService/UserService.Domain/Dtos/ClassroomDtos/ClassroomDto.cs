namespace UserService.Domain.Dtos.ClassroomDtos;

public record ClassroomDto(
    Guid Id,
    string Name,
    string Code,
    string? Description,
    Guid TeacherId,
    string TeacherUserName,
    DateTime CreatedAt,
    List<StudentDto> Students);