namespace QLDT.Domain.Common;

public interface ISoftDelete
{
    Guid? DeletedBy { get; set; }
    DateTime? DeletedAt { get; set; }
}

public interface IAuditCreate
{
    DateTime CreatedAt { get; set; }
    Guid CreatedBy { get; set; }
}

public interface IAuditModify
{
    DateTime? UpdatedAt { get; set; }
    Guid? UpdatedBy { get; set; }
}

public abstract class BaseEntity : IAuditCreate, IAuditModify, ISoftDelete
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
