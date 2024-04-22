using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace QLDT.Infrastructure.Persistence;

internal sealed class CustomUserStore : UserStore<IdentityUser>  
{
    public CustomUserStore(AppDbContext context)
    : base(context)
    {
        AutoSaveChanges = false;
    }
}
