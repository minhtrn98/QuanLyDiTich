namespace QLDT.Domain.Users.Repository;

public interface IFunctionRepository
{
    Task<IEnumerable<Permission>> GetPermissions(string functionName, CancellationToken cancellationToken);
    Task<IEnumerable<string>> GetPermissionNames(string functionName, CancellationToken cancellationToken);
}
