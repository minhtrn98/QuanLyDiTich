using Microsoft.AspNetCore.Identity;
using QLDT.Domain.Common;

namespace QLDT.Infrastructure.Identity;

internal sealed class AppRoleClaim : IdentityRoleClaim<Guid>, IAuditCreate
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}
