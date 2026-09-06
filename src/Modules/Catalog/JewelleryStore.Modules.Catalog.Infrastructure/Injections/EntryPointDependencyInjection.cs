using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
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

    public static IApplicationBuilder UseCatalogExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}