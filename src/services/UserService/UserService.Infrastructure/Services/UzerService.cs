using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Infrastructure.Interfaces;
using UserService.Application.Interfaces;
using UserService.Application.Interfaces.Auth;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Services;

public class UzerService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public UzerService(
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task Register(
        string username,
        string email,
        string password)
    {
        var existingUser = await _userRepository.GetByUsername(username);
        if (existingUser != null) throw new Exception("Username already exists");
        
        var existingEmail = await _userRepository.GetByUsername(email);
        if (existingEmail != null) throw new Exception("Email already exists");
        
        var hashedPassword = _passwordHasher.Generate(password);
        User user = new(Guid.NewGuid(), Role.Student, username, email, hashedPassword);
        await _userRepository.Add(user);
    }

    public async Task<string> Login(string username, string password)
    {
        var user = await _userRepository.GetByUsername(username);
        var result = _passwordHasher.Verify(password, user.PasswordHash);
        if (!result) throw new Exception("Invalid password or username");

        return _jwtProvider.GenerateToken(user);
    }
}
