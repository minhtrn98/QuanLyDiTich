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

        //builder.Property(pc => pc.FirstName)
        //    .HasMaxLength(UserSchema.FIRST_NAME_MAX_LENGTH)
        //    .IsRequired(true);

        //builder.Property(pc => pc.LastName)
        //    .HasMaxLength(UserSchema.LAST_NAME_MAX_LENGTH)
        //    .IsRequired(true);

        //builder.HasMany(e => e.Permissions)
        //    .WithMany()
        //    .UsingEntity<UserPermission>();

        //builder.HasMany(e => e.Roles)
        //    .WithMany()
        //    .UsingEntity<UserRole>();
    }
}
