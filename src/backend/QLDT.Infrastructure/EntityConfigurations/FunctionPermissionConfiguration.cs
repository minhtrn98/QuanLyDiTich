using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLDT.Domain.Users;

namespace QLDT.Infrastructure.EntityConfigurations;

internal sealed class FunctionPermissionConfiguration : IEntityTypeConfiguration<FunctionPermission>
{
    public void Configure(EntityTypeBuilder<FunctionPermission> builder)
    {
        builder.ToTable("FunctionPermissions");

        builder.HasKey(t => t.Id)
            .IsClustered(false);
    }
}
