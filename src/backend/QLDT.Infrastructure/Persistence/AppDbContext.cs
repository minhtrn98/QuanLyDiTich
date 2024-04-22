using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using QLDT.Application.Common.Services;
using QLDT.Domain.Common;
using QLDT.Infrastructure.EntityConfigurations;
using QLDT.Infrastructure.Identity;

namespace QLDT.Infrastructure.Persistence;

internal sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole, IdentityUserLogin<Guid>, AppRoleClaim, IdentityUserToken<Guid>>(options)
{
    public IContextUserService ContextUserService { get; set; } = default!;
    public IDateTimeService DateTimeService { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new RoleClaimConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new FunctionPermissionConfiguration());
        modelBuilder.ApplyConfiguration(new FunctionConfiguration());

        base.OnModelCreating(modelBuilder);
        RemoveAspNetPrefixTableName(modelBuilder);
    }

    private static void RemoveAspNetPrefixTableName(ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            string? tableName = entityType.GetTableName();
            if (string.IsNullOrEmpty(tableName))
            {
                continue;
            }

            if (tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName["AspNet".Length..]);
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        Guid currentUserId = ContextUserService.GetCurrentUser().Id;

        // create
        IEnumerable<EntityEntry> createEntries = ChangeTracker.Entries()
            .Where(e => e is { Entity: IAuditCreate, State: EntityState.Added });
        foreach (EntityEntry? entityEntry in createEntries)
        {
            ((IAuditCreate)entityEntry.Entity).CreatedAt = DateTimeService.UtcNow;
            ((IAuditCreate)entityEntry.Entity).CreatedBy = currentUserId;
        }

        // update
        IEnumerable<EntityEntry> updateEntries = ChangeTracker.Entries()
            .Where(e => e is { Entity: IAuditModify, State: EntityState.Modified });
        foreach (EntityEntry? entityEntry in updateEntries)
        {
            ((IAuditModify)entityEntry.Entity).UpdatedAt = DateTimeService.UtcNow;
            ((IAuditModify)entityEntry.Entity).UpdatedBy = currentUserId;
        }

        // delete
        IEnumerable<EntityEntry> deleteEntries = ChangeTracker.Entries()
            .Where(e => e is { Entity: ISoftDelete, State: EntityState.Deleted });
        foreach (EntityEntry? deleteEntry in deleteEntries)
        {
            ((ISoftDelete)deleteEntry.Entity).DeletedAt = DateTimeService.UtcNow;
            ((ISoftDelete)deleteEntry.Entity).DeletedBy = currentUserId;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
