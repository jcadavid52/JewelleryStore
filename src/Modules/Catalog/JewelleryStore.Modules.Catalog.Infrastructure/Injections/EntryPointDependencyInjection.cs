using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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