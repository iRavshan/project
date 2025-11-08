using System.Net;

namespace UserService.Infrastructure.Exceptions;

public class UserNotFoundException(string username)
    : BaseException($"User '{username}' not found.", HttpStatusCode.NotFound);