using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserService.Infrastructure.Interfaces;
using UserService.Application.Interfaces;
using UserService.Application.Interfaces.Auth;
using UserService.Domain.Entities;
using UserService.Infrastructure.Exceptions;

namespace UserService.Infrastructure.Services;

public class UzerService(
    UserManager<User> userManager,
    IJwtProvider jwtProvider,
    RoleManager<IdentityRole<Guid>>  roleManager)
    : IUserService
{
    public async Task Register(
        string username,
        string email,
        string password,
        string? firstName = null,
        string? lastName = null,
        string? phone = null,
        string role = "Student")
    {
        var existingUser = await userManager.FindByNameAsync(username);
        if (existingUser != null) throw new UsernameAlreadyExistsException(username);
        
        var existingEmail = await userManager.FindByEmailAsync(email);
        if (existingEmail != null) throw new EmailAlreadyExistsException(email);
        
        var user = new User()
        { 
            UserName = username,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phone,
            DateJoined = DateTime.UtcNow
        };
        
        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"User registration failed: {errors}");
        }
        
        var roleResult = await userManager.AddToRoleAsync(user, role);
        if (!roleResult.Succeeded)
        {
            await userManager.DeleteAsync(user);
            var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
            throw new Exception($"Role assignment failed: {errors}");
        }
    }

    public async Task<string> Login(string username, string password)
    {
        var user = await userManager.FindByNameAsync(username);

        if (user == null || !await userManager.CheckPasswordAsync(user, password))
            throw new InvalidCredentialsGivenException();

        var roles = await userManager.GetRolesAsync(user);
        
        return jwtProvider.GenerateToken(user, roles);
    }
}
