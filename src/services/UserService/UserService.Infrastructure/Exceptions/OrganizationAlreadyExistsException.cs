using System.Net;

namespace UserService.Infrastructure.Exceptions;

public class OrganizationAlreadyExistsException(string orgName)
    : BaseException($"Organization '{orgName}' is already in use.", HttpStatusCode.Conflict);