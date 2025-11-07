using UserService.Domain.Entities;

namespace UserService.Application.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(User user);
}