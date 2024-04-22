namespace QLDT.Domain.Users;

public sealed class FunctionPermission : IAuditCreate
{
    public Guid Id { get; init; }

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; } = default!;

    public Guid FunctionId { get; set; }
    public Function Function { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}
