using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QLDT.Application.Common.Services;
using QLDT.Domain;
using QLDT.Domain.Common;
using QLDT.Domain.Users.Repository;
using QLDT.Infrastructure.Common.Caching;
using QLDT.Infrastructure.Common.Services;
using QLDT.Infrastructure.Identity;
using QLDT.Infrastructure.Security;
using QLDT.Infrastructure.Security.ContextUser;
using QLDT.Infrastructure.Security.Token;
using QLDT.Infrastructure.Users.Persistence;

namespace QLDT.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddServices(configuration)
            .AddPersistence(configuration)
            .AddAuthorization()
            .AddAuthentication()
            .AddInMemoryCache();

        return services;
    }

    private static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetRequiredSection(JwtSettings.NAME))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IDateTimeService, SystemDateTimeProvider>();

        return services;
    }

    private static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IUserStore<IdentityUser>, CustomUserStore>();
        services.AddPooledDbContextFactory<AppDbContext>(options =>
        {
            options.UseSqlServer(
                connectionString: configuration.GetConnectionString("Default"),
                sqlServerOptionsAction: optionBuilder =>
                    optionBuilder
                    .ExecutionStrategy(dependencies =>
                        new SqlServerRetryingExecutionStrategy(
                            dependencies: dependencies,
                            maxRetryCount: 3,
                            maxRetryDelay: TimeSpan.FromSeconds(1000),
                            errorNumbersToAdd: [3])
                    ).MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name)
                );
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        });
        services.AddScoped<AppDbContextScopedFactory>();
        services.AddScoped(sp => sp.GetRequiredService<AppDbContextScopedFactory>().CreateDbContext());

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDapperDbConnection, DapperSqlServerDbConnection>();

        return services;
    }

    private static IServiceCollection AddAuthorization(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IContextUserService, ContextUserService>();

        // add identity
        IdentityBuilder identityBuilder = services.AddIdentityCore<AppUser>(o =>
        {
            // configure identity options
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequiredLength = 6;
        });

        identityBuilder = new IdentityBuilder(
            identityBuilder.UserType,
            typeof(AppRole),
            identityBuilder.Services);
        identityBuilder
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services
            .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

        return services;
    }

    private static IServiceCollection AddInMemoryCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
        return services;
    }
}
