using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLDT.Infrastructure.Identity;

namespace QLDT.Infrastructure.EntityConfigurations;

internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        builder.ToTable("AppUserRoles");

        builder.HasKey(t => t.Id)
            .IsClustered(false);
    }
}
