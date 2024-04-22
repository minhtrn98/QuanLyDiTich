namespace QLDT.Domain.Users.Repository;

public interface IRoleRepository
{
    Task<bool> Create(Role role);
    Task<IEnumerable<Role>> Find();
    Task<bool> Delete(Guid roleId);
}
