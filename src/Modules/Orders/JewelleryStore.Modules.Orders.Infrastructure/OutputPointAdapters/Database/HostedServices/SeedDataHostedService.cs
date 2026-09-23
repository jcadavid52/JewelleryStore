using JewelleryStore.Modules.Orders.Domain.Entities;
using JewelleryStore.Modules.Orders.Domain.ValueObjects;
using JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.Database.SeedData;
using JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.Database.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.Database.HostedServices;

public class SeedDataHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public SeedDataHostedService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var provider = _serviceProvider.CreateAsyncScope();
        var dbContext = provider.ServiceProvider.GetRequiredService<OrdersDbContext>();

        if (await dbContext.Orders.AnyAsync(cancellationToken))
            return;

        foreach (var seedOrder in OrdersSeedData.Orders)
        {
            var order = new Order(
                seedOrder.CustomerId,
                new ShippingAddress(
                    seedOrder.Address,
                    seedOrder.City,
                    seedOrder.PostalCode,
                    seedOrder.Phone));

            foreach (var item in seedOrder.Items)
                order.AddOrderItem(item.ProductId, item.Quantity, item.UnitPrice);

            dbContext.Orders.Add(order);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}