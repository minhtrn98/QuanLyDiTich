using Microsoft.EntityFrameworkCore;
using QLDT.Application.Common.Services;
using QLDT.Domain.Common;

namespace QLDT.Infrastructure.Persistence;

internal sealed class AppDbContextScopedFactory : IDbContextFactory<AppDbContext>
{
    private readonly IDbContextFactory<AppDbContext> _pooledFactory;
    private readonly IContextUserService _contextUserService;
    private readonly IDateTimeService _dateTimeService;

    public AppDbContextScopedFactory(
        IDbContextFactory<AppDbContext> pooledFactory,
        IContextUserService contextUserService,
        IDateTimeService dateTimeService)
    {
        _pooledFactory = pooledFactory;
        _contextUserService = contextUserService;
        _dateTimeService = dateTimeService;
    }

    public AppDbContext CreateDbContext()
    {
        AppDbContext context = _pooledFactory.CreateDbContext();
        context.ContextUserService = _contextUserService;
        context.DateTimeService = _dateTimeService;
        return context;
    }
}
