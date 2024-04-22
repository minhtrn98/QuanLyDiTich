namespace QLDT.Domain.Users;

public sealed class Permission : BaseEntity
{
    public string Name { get; set; } = default!;
    public ICollection<Function> Functions { get; } = [];
    public ICollection<FunctionPermission> FunctionPermissions { get; } = [];
}
