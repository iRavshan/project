namespace UserService.Application.Interfaces;

public interface IUserService
{
    Task Register(string username, string email, string password);
    Task<string> Login(string username, string password);
}