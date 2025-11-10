using System.Net;

namespace UserService.Infrastructure.Exceptions;

public class OrganizationNotFoundException(Guid organizationId)
    : BaseException($"Organization '{organizationId}' not found.", HttpStatusCode.NotFound);