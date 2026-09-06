using Microsoft.Extensions.DependencyInjection;

namespace JewelleryStore.Modules.Catalog.Infrastructure.Injections;

public static class DependencyInjection
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(DependencyInjection).Assembly);

        return services;
    }
}
