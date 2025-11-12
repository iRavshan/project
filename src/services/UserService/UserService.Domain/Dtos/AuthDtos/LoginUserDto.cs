using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Dtos;

public record LoginUserDto(
    [Required] string UserName,
    [Required] string Password);
