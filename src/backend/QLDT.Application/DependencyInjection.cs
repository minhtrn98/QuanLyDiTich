using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using QLDT.Application.Common.Behaviors;
using QLDT.Application.Localization;
using System.Reflection;

namespace QLDT.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
            options.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            options.AddOpenBehavior(typeof(LoggingBehavior<,>));
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddLocalization();
        return services;
    }

    private static IServiceCollection AddLocalization(this IServiceCollection services)
    {
        string resourcePath = "Resources";
#if DEBUG
        resourcePath = Directory.GetCurrentDirectory()[..^"API".Length] + "Application\\Resources";
#endif
        services.AddLocalization(options => options.ResourcesPath = resourcePath);
        services.Configure<RequestLocalizationOptions>(options =>
        {
            // base on resource file in Resource folder to add cultures
            string[] supportedCultures = Directory.GetFiles(resourcePath)
                .Select(path => path.Split(resourcePath + "\\")[1].Split('.')[0])
                .ToArray();
            options
                .SetDefaultCulture("vi-VN")
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            options.ApplyCurrentCultureToResponseHeaders = true;
        });
        services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
        return services;
    }
}
