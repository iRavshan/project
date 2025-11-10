using System.Net;
using Microsoft.AspNetCore.Identity;

namespace UserService.Infrastructure.Exceptions;

public class AdminCreationFailedException(IEnumerable<IdentityError> resultErrors) 
    : BaseException($"Admin creation failed because: {resultErrors}", HttpStatusCode.BadRequest);