using Microsoft.OpenApi.Models;
using System.Diagnostics;

namespace QLDT.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddProblemDetails()
            .AddSwagger()
            .AddCors()
            .AddSpaYarp();

        services
            .AddControllers(config =>
            {
                config.ReturnHttpNotAcceptable = true;
            })
            .AddConfigCustomJsonOptions();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.CustomSchemaIds(type => type.FullName);
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

        });

        return services;
    }

    private static IServiceCollection AddProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        options.CustomizeProblemDetails = ctx =>
        {
            //https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.Core/src/Infrastructure/DefaultProblemDetailsFactory.cs
            if (!ctx.ProblemDetails.Extensions.ContainsKey("traceId"))
            {
                ctx.ProblemDetails.Extensions.Add("traceId", Activity.Current?.Id ?? ctx.HttpContext.TraceIdentifier);
            }
            ctx.ProblemDetails.Extensions.Add("instance", $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
        });
        services.AddExceptionHandler<ExceptionToProblemDetailsHandler>();
        return services;
    }

    public static IServiceCollection AddCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("dev-cors",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            options.AddPolicy("prod-cors",
                builder => builder
                    //.WithOrigins(allowedCorsOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });

        return services;
    }

    public static IMvcBuilder AddConfigCustomJsonOptions(this IMvcBuilder mvcBuilder)
    {
        mvcBuilder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new CustomStringJsonConverter());
        });

        return mvcBuilder;
    }
}
