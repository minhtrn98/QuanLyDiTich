using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLDT.Domain.Users;

namespace QLDT.Infrastructure.EntityConfigurations;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(t => t.Id)
            .IsClustered(false);

        builder.Property(u => u.Name)
        .HasMaxLength(255);

        builder
            .HasMany(e => e.Functions)
            .WithMany(e => e.Permissions)
            .UsingEntity<FunctionPermission>(
            l => l.HasOne(e => e.Function).WithMany(e => e.FunctionPermissions),
            r => r.HasOne(e => e.Permission).WithMany(e => e.FunctionPermissions));
    }
}
