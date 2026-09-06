using JewelleryStore.Modules.Catalog.Domain.OuputPorts;
using JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.HostedServices;
using JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer;
using JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JewelleryStore.Modules.Catalog.Infrastructure.Injections;

public static class OutputPointDependencyInjection
{
    public static IServiceCollection AddCatalogPersistence(this IServiceCollection services, string connectionString, bool applyMigrations)
    {
        services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, SqlServerUnitOfWork>();

        if (applyMigrations)
        {
            services.AddHostedService<AutomaticMigrationsHostedService>();
        }

        return services;
    }
}