using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using JewelleryStore.Modules.Catalog.Infrastructure.EntryPointAdapters.Rest.Middlewares;

namespace JewelleryStore.Modules.Catalog.Infrastructure.Injections;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(DependencyInjection).Assembly);

        return services;
    }

    public static IApplicationBuilder UseCatalogExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
