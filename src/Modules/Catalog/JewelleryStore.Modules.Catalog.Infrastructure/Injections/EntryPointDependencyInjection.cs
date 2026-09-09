using System.Text.Json;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using JewelleryStore.Modules.Catalog.Infrastructure.EntryPointAdapters.Rest.Middlewares;

namespace JewelleryStore.Modules.Catalog.Infrastructure.Injections;

public static class EntryPointDependencyInjection
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(EntryPointDependencyInjection).Assembly);

        return services;
    }

    public static IServiceCollection AddCatalogOpenApi(this IServiceCollection services)
    {
        services.AddOpenApi();

        return services;
    }

    public static IApplicationBuilder UseCatalogExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseCatalogSecurity(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SecurityMiddleware>();
    }

    public static IServiceCollection AddCatalogRateLimiting(
        this IServiceCollection services,
        bool enabled,
        int permitLimit,
        int windowSeconds,
        int queueLimit)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.OnRejected = async (context, cancellationToken) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    context.HttpContext.Response.Headers.RetryAfter = ((int)retryAfter.TotalSeconds).ToString();

                context.HttpContext.Response.ContentType = "application/problem+json";

                var problemDetails = new
                {
                    type = "https://httpstatuses.com/429",
                    title = "Too Many Requests",
                    status = StatusCodes.Status429TooManyRequests,
                    detail = "Se han superado los límites de peticiones. Intente de nuevo más tarde.",
                    instance = context.HttpContext.Request.Path.ToString()
                };

                var json = JsonSerializer.Serialize(problemDetails);
                await context.HttpContext.Response.WriteAsync(json, cancellationToken);
            };

            options.AddPolicy("ApiRateLimit", context =>
            {
                if (!enabled)
                    return RateLimitPartition.GetNoLimiter("ApiRateLimit");

                var partitionKey = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                return RateLimitPartition.GetFixedWindowLimiter(partitionKey, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = permitLimit,
                    Window = TimeSpan.FromSeconds(windowSeconds),
                    QueueLimit = queueLimit,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    AutoReplenishment = true
                });
            });
        });

        return services;
    }

    public static IApplicationBuilder UseCatalogRateLimiting(this IApplicationBuilder app)
    {
        return app.UseRateLimiter();
    }

    public static IEndpointRouteBuilder MapCatalogOpenApi(this IEndpointRouteBuilder endpoints)
    {
        var environment = endpoints.ServiceProvider.GetService<IWebHostEnvironment>();

        if (environment is not null &&
            (environment.IsEnvironment("Local") || environment.IsEnvironment(Environments.Development)))
        {
            endpoints.MapOpenApi();
            endpoints.MapScalarApiReference();
        }

        return endpoints;
    }
}