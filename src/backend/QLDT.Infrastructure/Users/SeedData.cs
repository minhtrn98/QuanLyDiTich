
using QLDT.Domain.Users;

namespace QLDT.Infrastructure.Users;

internal sealed class SeedData
{
    public static readonly IReadOnlyList<User> SeedUsers =
    [
        new User() { Id = Guid.NewGuid(), FirstName = "tony", LastName = "stark" },
        new User() { Id = Guid.NewGuid(), FirstName = "tom", LastName = "holand" },
    ];
}
