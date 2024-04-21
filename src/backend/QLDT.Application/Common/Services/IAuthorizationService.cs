namespace QLDT.Application.Common.Services;

public interface IAuthorizationService
{
    Result<Success> AuthorizeCurrentUser(
        IEnumerable<string> requiredRoles,
        IEnumerable<string> requiredPermissions);
}
