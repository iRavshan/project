using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Dtos;

public record LoginUserDto(
    [Required] string userName,
    [Required] string password);
