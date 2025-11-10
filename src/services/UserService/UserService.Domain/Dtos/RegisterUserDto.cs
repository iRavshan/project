using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Dtos;

public record RegisterUserDto(
    [Required] string UserName,
    [Required] string Email,
    [Required] string Password,
    string? FirstName,
    string? LastName,
    string? Phone);