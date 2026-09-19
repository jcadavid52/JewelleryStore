using JewelleryStore.Modules.Inventory.Domain.OuputPorts;
using JewelleryStore.Modules.Inventory.Infrastructure.OutputPointAdapters.Database.HostedServices;
using JewelleryStore.Modules.Inventory.Infrastructure.OutputPointAdapters.Database.SqlServer;
using JewelleryStore.Modules.Inventory.Infrastructure.OutputPointAdapters.Database.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JewelleryStore.Modules.Inventory.Infrastructure.Injections;

public static class OutputPointDependencyInjection
{
    public static IServiceCollection AddInventoryPersistence(
        this IServiceCollection services,
        string connectionString,
        bool applyMigrations = false,
        bool seedDataOnStartup = false)
    {
        services.AddDbContext<InventoryDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IStockItemRepository, StockItemRepository>();
        services.AddScoped<IUnitOfWork, SqlServerUnitOfWork>();

        if (applyMigrations)
        {
            services.AddHostedService<AutomaticMigrationsHostedService>();
        }

        if (seedDataOnStartup)
        {
            services.AddHostedService<SeedDataHostedService>();
        }

        return services;
    }
}
