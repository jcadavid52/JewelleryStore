using JewelleryStore.Modules.Orders.Application.Services;
using JewelleryStore.Modules.Orders.Domain.OuputPorts;
using JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.Database.HostedServices;
using JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.Database.SqlServer;
using JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.Database.SqlServer.Repositories;
using JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.InProcess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JewelleryStore.Modules.Orders.Infrastructure.Injections;

public static class OutputPointDependencyInjection
{
    public static IServiceCollection AddOrdersPersistence(
        this IServiceCollection services,
        string connectionString,
        bool applyMigrations = false,
        bool seedDataOnStartup = false)
    {
        services.AddDbContext<OrdersDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IOrderRepository, OrderRepository>();
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

    public static IServiceCollection AddOrdersStockReservation(this IServiceCollection services)
    {
        services.AddScoped<IStockReservationService, StockReservationServiceAdapter>();

        return services;
    }
}