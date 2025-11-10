using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Dtos;
using UserService.Infrastructure.Interfaces;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/organizations/{orgId}/[controller]")]
[Authorize(Roles = "SuperAdmin, Admin")]
public class TeachersController(
    ITeacherService teacherService,
    ILogger<TeachersController> logger)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateTeacher(Guid orgId, [FromBody] CreateTeacherDto dto)
    {
        logger.LogInformation("Creating teacher for organization {OrganizationId} by user {User}", 
            orgId, User.Identity?.Name);

        var result = await teacherService.CreateTeacherAsync(orgId, dto);
        
        logger.LogInformation("Teacher {UserName} created successfully for organization {OrganizationId}", 
            dto.UserName, orgId);
        
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetTeachers(Guid orgId)
    {
        logger.LogInformation("Fetching teachers for organization {OrganizationId} by user {User}", 
            orgId, User.Identity?.Name);

        var teachers = await teacherService.GetTeachersByOrganizationAsync(orgId);
        
        logger.LogInformation("Retrieved {Count} teachers for organization {OrganizationId}", 
            teachers.Count, orgId);
        
        return Ok(teachers);
    }

    [HttpGet("{teacherId}")]
    public async Task<IActionResult> GetTeacher(Guid orgId, Guid teacherId)
    {
        logger.LogInformation("Fetching teacher {TeacherId} for organization {OrganizationId}", 
            teacherId, orgId);

        var teacher = await teacherService.GetTeacherByIdAsync(teacherId);
        return Ok(teacher);
    }
}