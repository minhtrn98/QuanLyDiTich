using System.Data;

namespace QLDT.Domain;

public interface IDapperDbConnection : IDisposable
{
    IDbConnection CreateConnection();
}
