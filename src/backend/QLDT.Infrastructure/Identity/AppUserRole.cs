using Microsoft.AspNetCore.Identity;
using QLDT.Domain.Common;

namespace QLDT.Infrastructure.Identity;

internal sealed class AppUserRole : IdentityUserRole<Guid>, IAuditCreate
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
}
