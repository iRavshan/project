using Microsoft.AspNetCore.Http;
using UserService.Domain.Dtos;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Interfaces;

public interface IOrganizationService
{
    Task<OrganizationCreateDto> CreateOrganizationAsync(OrganizationCreateDto dto);
    
    Task<AdminDto> CreateAdminAsync(Guid orgId, CreateAdminDto dto);
}