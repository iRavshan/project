using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Dtos.ClassroomDtos;

public record EnrollStudentDto([Required] Guid StudentId);