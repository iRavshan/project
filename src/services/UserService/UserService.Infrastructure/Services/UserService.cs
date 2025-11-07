using UserService.Application.Interfaces;
using UserService.Application.Interfaces.Auth;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public UserService(
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
        var hashedPassword = _passwordHasher.Generate(password);
        User user = new (Guid.NewGuid(), Role.Student,  username, email, hashedPassword);
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
