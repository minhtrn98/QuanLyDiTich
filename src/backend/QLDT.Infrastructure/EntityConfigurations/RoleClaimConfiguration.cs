using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLDT.Infrastructure.Identity;

namespace QLDT.Infrastructure.EntityConfigurations;

internal sealed class RoleClaimConfiguration : IEntityTypeConfiguration<AppRoleClaim>
{
    public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
    {
        builder.ToTable("AppRoleClaims");

        builder.HasKey(t => t.Id)
            .IsClustered(false);
    }
}
