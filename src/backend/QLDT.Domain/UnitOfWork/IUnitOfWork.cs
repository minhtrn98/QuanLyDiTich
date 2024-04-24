using QLDT.Domain.Users.Repository;

namespace QLDT.Domain.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IFunctionRepository Functions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
