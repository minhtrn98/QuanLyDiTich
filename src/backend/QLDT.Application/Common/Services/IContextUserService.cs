namespace QLDT.Application.Common.Services;

public sealed record CurrentUser(
    Guid Id,
    IReadOnlyList<string> Permissions,
    IReadOnlyList<string> Roles);

public interface IContextUserService
{
    CurrentUser GetCurrentUser();
}
