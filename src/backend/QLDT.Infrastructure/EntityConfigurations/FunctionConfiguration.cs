using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLDT.Domain.Users;

namespace QLDT.Infrastructure.EntityConfigurations;

internal sealed class FunctionConfiguration : IEntityTypeConfiguration<Function>
{
    public void Configure(EntityTypeBuilder<Function> builder)
    {
        builder.ToTable("Functions");

        builder.HasIndex(u => u.Name).IsUnique(true);

        builder.HasKey(t => t.Id)
            .IsClustered(false);

        builder.Property(u => u.Name)
            .HasMaxLength(255);
    }
}
