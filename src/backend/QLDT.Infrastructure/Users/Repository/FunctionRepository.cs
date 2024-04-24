using Dapper;
using Microsoft.EntityFrameworkCore;
using QLDT.Domain;
using QLDT.Domain.Users;
using QLDT.Domain.Users.Repository;
using System.Data;

namespace QLDT.Infrastructure.Users.Persistence;

internal sealed class FunctionRepository : IFunctionRepository
{
    private readonly AppDbContext _dbContext;
    private readonly IDapperDbConnection _dbConnection;

    public FunctionRepository(AppDbContext dbContext, IDapperDbConnection dbConnection)
    {
        _dbContext = dbContext;
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Permission>> GetPermissions(
        string functionName, CancellationToken cancellationToken)
    {
        List<Permission> permissions = await _dbContext.Functions
            .Where(f => f.Name == functionName)
            .SelectMany(f => f.Permissions)
            .ToListAsync(cancellationToken);

        return permissions;
    }

    public async Task<IEnumerable<string>> GetPermissionNames(
        string functionName, CancellationToken cancellationToken)
    {
        string sql = @"
            select p.[Name]
            from Functions f
            join FunctionPermissions fp on fp.FunctionId = f.Id
            join Permissions p on fp.PermissionId = p.Id
            where f.[Name] = @functionName
        ";

        using IDbConnection dbConnection = _dbConnection.CreateConnection();
        IEnumerable<string> permissions = await dbConnection
            .QueryAsync<string>(sql, new { functionName });

        return permissions;
    }
}
