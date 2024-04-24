using XResult;

namespace QLDT.Domain.Users.Repository;

public interface IUserRepository
{
    Task<Result<Success>> Create(User user, string password);
    Task<User?> FindByName(string userName);
    Task<bool> CheckPassword(User user, string password);
}
