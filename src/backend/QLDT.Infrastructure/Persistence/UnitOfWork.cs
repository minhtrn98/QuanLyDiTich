using Microsoft.AspNetCore.Identity;
using QLDT.Domain;
using QLDT.Domain.Users.Repository;
using QLDT.Infrastructure.Identity;
using QLDT.Infrastructure.Users.Persistence;

namespace QLDT.Infrastructure.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly IUserRepository _user;
    private readonly IFunctionRepository _function;
    private readonly AppDbContext _dbContext;
    private readonly IDapperDbConnection _dbConnection;

    public UnitOfWork(AppDbContext dbContext, UserManager<AppUser> userManager, IDapperDbConnection dbConnection)
    {
        _dbContext = dbContext;
        _dbConnection = dbConnection;

        _user = new UserRepository(userManager);
        _function = new FunctionRepository(_dbContext, _dbConnection);
    }

    public IUserRepository Users => _user;
    public IFunctionRepository Functions => _function;

    public void Dispose()
    {
        _dbContext?.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
