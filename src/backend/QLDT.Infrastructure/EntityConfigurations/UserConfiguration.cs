using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLDT.Infrastructure.Identity;

namespace QLDT.Infrastructure.EntityConfigurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("AppUsers");

        builder.HasKey(t => t.Id)
            .IsClustered(false);

        builder.Property(u => u.FirstName)
            .HasMaxLength(255);

        builder.Property(u => u.LastName)
            .HasMaxLength(255);
    }
}
