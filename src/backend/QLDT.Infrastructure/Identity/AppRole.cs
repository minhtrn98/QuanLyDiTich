using Microsoft.AspNetCore.Identity;
using QLDT.Domain.Common;

namespace QLDT.Infrastructure.Identity;

internal sealed class AppRole : IdentityRole<Guid>, IAuditCreate, IAuditModify, ISoftDelete
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
}
