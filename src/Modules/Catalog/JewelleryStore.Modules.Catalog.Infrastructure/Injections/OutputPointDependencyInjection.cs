using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;
using JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer;
using JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer.Repositories;

namespace JewelleryStore.Modules.Catalog.Infrastructure.Injections;

public static class OutputPointDependencyInjection
{
    public static IServiceCollection AddCatalogPersistence(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitOfWork, SqlServerUnitOfWork>();

        return services;
    }
}