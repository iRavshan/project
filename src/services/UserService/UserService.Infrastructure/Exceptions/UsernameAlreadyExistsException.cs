using System.Net;

namespace UserService.Infrastructure.Exceptions;

public class UsernameAlreadyExistsException(string username)
    : BaseException($"Username '{username}' is already in use.", HttpStatusCode.Conflict);