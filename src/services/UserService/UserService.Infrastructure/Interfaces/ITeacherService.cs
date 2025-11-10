using UserService.Domain.Dtos;

namespace UserService.Infrastructure.Interfaces;

public interface ITeacherService
{
    Task<TeacherDto> CreateTeacherAsync(Guid organizationId, CreateTeacherDto dto);
    Task<List<TeacherDto>> GetTeachersByOrganizationAsync(Guid organizationId);
    Task<TeacherDto> GetTeacherByIdAsync(Guid teacherId);
}