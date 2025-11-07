using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Dtos;

public record RegisterUserDto(
    [Required] string userName,
    [Required] string email,
    [Required] string password);