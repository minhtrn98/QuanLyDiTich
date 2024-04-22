using Microsoft.AspNetCore.Identity;
using QLDT.Domain.Common;

namespace QLDT.Infrastructure.Identity;

internal sealed class AppUser : IdentityUser<Guid>, IAuditCreate, IAuditModify
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime? Dob { get; set; }

    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}
