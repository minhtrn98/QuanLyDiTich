namespace QLDT.Domain.Users;

public sealed class Function : BaseEntity
{
    public string Name { get; set; } = default!;
    public ICollection<Permission> Permissions { get; } = [];
    public ICollection<FunctionPermission> FunctionPermissions { get; } = [];
}
