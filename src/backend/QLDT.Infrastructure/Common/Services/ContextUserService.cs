using Microsoft.AspNetCore.Http;
using QLDT.Application.Common.Services;
using System.Security.Claims;

namespace QLDT.Infrastructure.Security.ContextUser;

internal sealed class ContextUserService(IHttpContextAccessor _httpContextAccessor) : IContextUserService
{
    public CurrentUser GetCurrentUser()
    {
        HttpContext httpContext = _httpContextAccessor.HttpContext
            ?? throw new Exception("Cannot access HttpContext");

        bool isSuccess = Guid.TryParse(GetSingleClaimValue(httpContext, "id"), out Guid id);

        if (isSuccess)
        {
            List<string> permissions = GetClaimValues(httpContext, "permissions");
            List<string> roles = GetClaimValues(httpContext, ClaimTypes.Role);
            return new CurrentUser(id, permissions, roles);
        }
#if DEBUG
        return new CurrentUser(Guid.Empty, [], []);
#endif
        throw new Exception("UserId is not valid");
    }

    private static List<string> GetClaimValues(HttpContext httpContext, string claimType) =>
        httpContext.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();

    private static string GetSingleClaimValue(HttpContext httpContext, string claimType) =>
        httpContext.User.Claims
            .SingleOrDefault(claim => claim.Type == claimType)
            ?.Value ?? string.Empty;
}
