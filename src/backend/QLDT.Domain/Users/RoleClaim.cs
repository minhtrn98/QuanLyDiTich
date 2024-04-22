namespace QLDT.Domain.Users;

public sealed class RoleClaim : IAuditCreate
{
    public Guid Id { get; init; }

    public Guid RoleId { get; set; }
    public Role Role { get; set; } = default!;

    public string ClaimType { get; set; } = default!;
    public string ClaimValue { get; set; } = default!;

    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}
