using QLDT.API;
using QLDT.Application;
using QLDT.Infrastructure;
using Microsoft.Net.Http.Headers;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    ConfigureHostBuilder host = builder.Host;
    IServiceCollection services = builder.Services;

    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.AddServerHeader = false;
    });

    host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Filter
                    .ByExcluding(logEvent => logEvent.Exception is
                        FluentValidation.ValidationException or TaskCanceledException or OperationCanceledException)
                .Enrich.FromLogContext());

    services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

// Configure the HTTP request pipeline.
WebApplication app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseSerilogRequestLogging();

    app.UseExceptionHandler();
    app.UseStatusCodePages();

    app.UseHsts();
    app.UseHttpsRedirection();

    app.UseStaticFiles();

    if (app.Environment.IsDevelopment())
    {
        app.UseCors("dev-cors");
    }
    else
    {
        app.UseCors("prod-cors");
    }

    app.Use(async (context, next) =>
    {
        context.Response.GetTypedHeaders().CacheControl =
            new CacheControlHeaderValue()
            {
                NoCache = true,
                NoStore = true
            };
        context.Response.Headers[HeaderNames.Pragma] = "no-cache";
        context.Response.Headers[HeaderNames.Expires] = "0";
        context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        // context.Response.Headers[HeaderNames.ContentType] = "nosniff";
        // context.Response.Headers[HeaderNames.ContentSecurityPolicy] =
        // "default-src 'none'; script-src 'self'; connect-src 'self' https://localhost:5001/; img-src 'self'; style-src 'self'; frame-ancestors 'self'; form-action 'self';";
        await next();
    });

    app.UseRequestLocalization();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    if (app.Environment.IsDevelopment())
    {
        app.UseSpaYarp();
    }
    else
    {
        app.MapFallbackToFile("index.html");
    }

    app.Run();
}
