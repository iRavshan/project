using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Application.Contexts;
using UserService.Domain.Dtos;
using UserService.Domain.Entities;
using UserService.Infrastructure.Exceptions;
using UserService.Infrastructure.Interfaces;

namespace UserService.Infrastructure.Services;

public class TeacherService(
    UserManager<User> userManager,
    AppDbContext context,
    ILogger<TeacherService> logger)
    : ITeacherService
{

    public async Task<TeacherDto> CreateTeacherAsync(Guid organizationId, CreateTeacherDto dto)
    {
        logger.LogInformation("Creating teacher for organization {OrganizationId}", organizationId);

        var organization = await context.Organizations.FindAsync(organizationId);
        if (organization == null)
        {
            logger.LogWarning("Organization {OrganizationId} not found", organizationId);
            throw new OrganizationNotFoundException(organizationId);
        }

        var existingUser = await userManager.FindByNameAsync(dto.UserName);
        if (existingUser != null)
        {
            logger.LogWarning("Username {UserName} already exists", dto.UserName);
            throw new UsernameAlreadyExistsException(dto.UserName);
        }

        var existingEmail = await userManager.FindByEmailAsync(dto.Email);
        if (existingEmail != null)
        {
            logger.LogWarning("Email {Email} already exists", dto.Email);
            throw new EmailAlreadyExistsException(dto.Email);
        }

        var teacher = new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.Phone,
            OrganizationId = organizationId,
            DateJoined = DateTime.UtcNow
        };

        var createResult = await userManager.CreateAsync(teacher, dto.Password);
        if (!createResult.Succeeded)
        {
            var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
            logger.LogError("Failed to create teacher: {Errors}", errors);
            throw new Exception($"Teacher creation failed: {errors}");
        }

        var roleResult = await userManager.AddToRoleAsync(teacher, "Teacher");
        if (!roleResult.Succeeded)
        {
            await userManager.DeleteAsync(teacher);
            var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
            logger.LogError("Failed to assign Teacher role: {Errors}", errors);
            throw new Exception($"Role assignment failed: {errors}");
        }

        logger.LogInformation("Teacher {UserName} created successfully for organization {OrganizationId}", 
            dto.UserName, organizationId);

        return new TeacherDto(
            teacher.Id,
            teacher.UserName,
            teacher.Email,
            teacher.FirstName,
            teacher.LastName,
            teacher.PhoneNumber,
            teacher.DateJoined);
    }

    public async Task<List<TeacherDto>> GetTeachersByOrganizationAsync(Guid organizationId)
    {
        logger.LogInformation("Fetching teachers for organization {OrganizationId}", organizationId);

        var teachers = await userManager.Users
            .Where(u => u.OrganizationId == organizationId)
            .Where(u => context.UserRoles
                .Join(context.Roles,
                    ur => ur.RoleId, 
                    r => r.Id, 
                    (ur, r) => new { ur.UserId, RoleName = r.Name })
                .Any(ur => ur.UserId == u.Id && ur.RoleName == "Teacher"))
            .Select(u => new TeacherDto(
                u.Id,
                u.UserName,
                u.Email,
                u.FirstName,
                u.LastName,
                u.PhoneNumber,
                u.DateJoined))
            .ToListAsync();

        logger.LogInformation("Found {Count} teachers for organization {OrganizationId}", 
            teachers.Count, organizationId);

        return teachers;
    }

    public async Task<TeacherDto> GetTeacherByIdAsync(Guid teacherId)
    {
        logger.LogInformation("Fetching teacher {TeacherId}", teacherId);

        var teacher = await userManager.Users
            .Where(u => u.Id == teacherId)
            .Where(u => context.UserRoles
                .Join(context.Roles, 
                    ur => ur.RoleId, 
                    r => r.Id, 
                    (ur, r) => new { ur.UserId, RoleName = r.Name })
                .Any(ur => ur.UserId == u.Id && ur.RoleName == "Teacher"))
            .Select(u => new TeacherDto(
                u.Id,
                u.UserName,
                u.Email,
                u.FirstName,
                u.LastName,
                u.PhoneNumber,
                u.DateJoined))
            .FirstOrDefaultAsync();

        if (teacher == null)
        {
            logger.LogWarning("Teacher {TeacherId} not found", teacherId);
            throw new Exception($"Teacher with ID {teacherId} not found");
        }

        return teacher;
    }
}