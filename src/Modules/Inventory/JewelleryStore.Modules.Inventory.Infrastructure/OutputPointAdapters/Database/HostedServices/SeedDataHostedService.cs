using JewelleryStore.Modules.Inventory.Domain.Entities;
using JewelleryStore.Modules.Inventory.Domain.OuputPorts;
using JewelleryStore.Modules.Inventory.Domain.ValueObjects;
using JewelleryStore.Modules.Inventory.Infrastructure.OutputPointAdapters.Database.SeedData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JewelleryStore.Modules.Inventory.Infrastructure.OutputPointAdapters.Database.HostedServices
{
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
            var stockItemRepository = provider.ServiceProvider.GetRequiredService<IStockItemRepository>();
            var unitOfWork = provider.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var existing = await stockItemRepository.GetAllAsync(cancellationToken);
            if (existing.Any())
                return;

            var stockItems = new List<StockItem>();
            foreach (var stockItem in StockItemSeedData.Items)
            {
                var entity = StockItem.Create(stockItem.ProductId);

                entity.ReceiveStock(new Quantity(stockItem.Quantity));

                stockItemRepository.Add(entity);
                stockItems.Add(entity);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}