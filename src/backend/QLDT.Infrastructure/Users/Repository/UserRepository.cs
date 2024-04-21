using QLDT.Domain.Users;
using QLDT.Domain.Users.Repository;

namespace QLDT.Infrastructure.Users.Persistence;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext.Users)
    {
    }
}
