using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Dtos.ClassroomDtos;

public record ClassroomCreateDto(
    [Required] string Name,
    [Required] string Code,
    string? Description);