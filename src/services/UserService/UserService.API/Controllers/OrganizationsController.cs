using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Dtos;
using UserService.Infrastructure.Interfaces;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SuperAdmin")]
public class OrganizationsController(IOrganizationService organizationService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] OrganizationCreateDto dto)
    {
        return Ok(await organizationService.CreateOrganizationAsync(dto));
    }
    
    [HttpPost("{orgId}/admins")]
    public async Task<IActionResult> CreateAdmin(Guid orgId, [FromBody] CreateAdminDto dto)
    {
        return Ok(await organizationService.CreateAdminAsync(orgId, dto));
    }
}