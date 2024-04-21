using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QLDT.Domain.Users;
using QLDT.Infrastructure.Users;

namespace QLDT.Infrastructure.EntityConfigurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(t => t.Id)
            .IsClustered(false);

        builder.Property(pc => pc.FirstName)
            .HasMaxLength(UserSchema.FIRST_NAME_MAX_LENGTH)
            .IsRequired(true);

        builder.Property(pc => pc.LastName)
            .HasMaxLength(UserSchema.LAST_NAME_MAX_LENGTH)
            .IsRequired(true);

        //builder.HasMany(e => e.Permissions)
        //    .WithMany()
        //    .UsingEntity<UserPermission>();

        //builder.HasMany(e => e.Roles)
        //    .WithMany()
        //    .UsingEntity<UserRole>();

        builder.HasData(SeedData.SeedUsers);
    }
}
