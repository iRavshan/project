namespace UserService.Domain.Dtos;

public record TeacherDto(
    Guid Id,
    string UserName,
    string Email,
    string? FirstName,
    string? LastName,
    string? Phone,
    DateTime DateJoined);