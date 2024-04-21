using QLDT.Application.Common.Services;
using QLDT.Application.Errors;
using XResult;

namespace QLDT.Infrastructure.Security;

internal sealed class AuthorizationService : IAuthorizationService
{
    private readonly IContextUserService _contextUserService;

    public AuthorizationService(IContextUserService contextUserService)
    {
        _contextUserService = contextUserService;
    }

    public Result<Success> AuthorizeCurrentUser(
        IEnumerable<string> requiredRoles,
        IEnumerable<string> requiredPermissions)
    {
        CurrentUser currentUser = _contextUserService.GetCurrentUser();

        if (requiredPermissions.Except(currentUser.Permissions).Any())
        {
            return AuthorizationErrors.MissingPermission;
        }

        if (requiredRoles.Except(currentUser.Roles).Any())
        {
            return AuthorizationErrors.MissingRole;
        }

        return Result.Success;
    }
}
