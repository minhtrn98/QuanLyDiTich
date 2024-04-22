namespace QLDT.Domain.Users;

public class UserRole : IAuditCreate
{
    public Guid Id { get; init; }

    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}
