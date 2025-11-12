using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Dtos.ClassroomDtos;

public record ClassroomUpdateDto(
    [Required] string Name,
    string? Description);