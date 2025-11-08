using System.Net;

namespace UserService.Infrastructure.Exceptions;

public class InvalidCredentialsGivenException() 
    : BaseException("Invalid username or password.", HttpStatusCode.Unauthorized);
