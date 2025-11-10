using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entities;

namespace UserService.Domain.Dtos;

public record CreateAdminDto(
    [Required] string UserName,
    [Required] string Password,
    [Required] Guid OrganizationId,
    [Required] string Role,
    [Required] string Phone,
    string FirstName,
    string LastName,
    string? Email = null);