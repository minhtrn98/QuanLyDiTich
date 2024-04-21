using QLDT.Domain.Users.Repository;
using QLDT.Infrastructure.Users.Persistence;

namespace QLDT.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly IUserRepository _user;
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _user = new UserRepository(dbContext);
        _dbContext = dbContext;
    }

    public IUserRepository Users => _user;

    public void Dispose()
    {
        _dbContext?.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
