using Microsoft.AspNetCore.Identity;

namespace UserService.Infrastructure.Services;

public class RoleService(RoleManager<IdentityRole<Guid>> roleManager)
{
    public async Task EnsureRolesExistAsync()
    {
        var roleNames = new[] { "SuperAdmin", "Admin", "Teacher", "Student" };
            
        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
        }
    }
}