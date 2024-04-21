using Microsoft.Extensions.Configuration;
using QLDT.Domain;
using System.Data;
using System.Data.SqlClient;

namespace QLDT.Infrastructure.Persistence;

internal sealed class DapperSqlServerDbConnection : IDapperDbConnection
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public DapperSqlServerDbConnection(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("Default")
            ?? throw new Exception("Cannot get connection string!");
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    public void Dispose()
    {

    }
}
