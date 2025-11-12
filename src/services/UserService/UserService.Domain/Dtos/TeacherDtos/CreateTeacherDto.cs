using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Dtos;

public record CreateTeacherDto(
    [Required] string UserName,
    [Required] string Email,
    [Required] string Password,
    string? FirstName = null,
    string? LastName = null,
    string? Phone = null);