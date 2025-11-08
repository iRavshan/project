using System.Net;

namespace UserService.Infrastructure.Exceptions;

public class EmailAlreadyExistsException(string email)
    : BaseException($"Email '{email}' is already in use.", HttpStatusCode.Conflict);