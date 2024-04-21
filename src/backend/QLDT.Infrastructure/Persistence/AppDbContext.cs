using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QLDT.Application.Common.Services;
using QLDT.Domain.Common;
using QLDT.Domain.Users;
using QLDT.Infrastructure.EntityConfigurations;

namespace QLDT.Infrastructure.Persistence;

internal sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public IContextUserService ContextUserService { get; set; } = default!;
    public IDateTimeService DateTimeService { get; set; } = default!;

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
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
