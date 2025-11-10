using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Contexts;
using UserService.Application.Interfaces.Auth;
using UserService.Domain.Dtos;
using UserService.Domain.Entities;
using UserService.Infrastructure.Exceptions;
using UserService.Infrastructure.Interfaces;

namespace UserService.Infrastructure.Services;

public class OrganizationService(
    AppDbContext context, 
    UserManager<User> userManager,
    RoleManager<IdentityRole<Guid>> roleManager) : IOrganizationService
{
    public async Task<OrganizationCreateDto> CreateOrganizationAsync(OrganizationCreateDto dto)
    {
        var existingOrg = await context.Organizations.FirstOrDefaultAsync(x => x.Name == dto.Name);
        if (existingOrg != null)
            throw new OrganizationAlreadyExistsException(dto.Name);
        
        var org = new Organization()
        {
            Name = dto.Name,
            Address = dto.Address
        };

        await context.Organizations.AddAsync(org);
        await context.SaveChangesAsync();
        
        return dto;
    }

    public async Task<AdminDto> CreateAdminAsync(Guid orgId, CreateAdminDto dto)
    {
        var organization = await context.Organizations.FindAsync(orgId);
        if (organization == null) throw new OrganizationNotFoundException(orgId);

        var existingUser = await userManager.FindByNameAsync(dto.UserName);
        if (existingUser != null) throw new UsernameAlreadyExistsException(dto.UserName);

        var admin = new User
        {
            UserName = dto.UserName,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.Phone,
            OrganizationId = organization.Id
        };

        var result = await userManager.CreateAsync(admin, dto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Admin creation failed: {errors}");
        }

        var roleResult = await userManager.AddToRoleAsync(admin, "Admin");
        if (!roleResult.Succeeded)
        {
            await userManager.DeleteAsync(admin);
            var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
            throw new Exception($"Role assignment failed: {errors}");
        }

        return new AdminDto(admin.UserName);
    }
}