namespace UserService.Domain.Dtos.ClassroomDtos;

public record StudentDto(
    Guid Id,
    string UserName,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Phone);