using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLDT.Infrastructure.Identity;

namespace QLDT.Infrastructure.EntityConfigurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable("AppRoles");

        builder.HasKey(t => t.Id)
            .IsClustered(false);
    }
}
