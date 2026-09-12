using JewelleryStore.Modules.Catalog.Domain.Entities;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;
using JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SeedData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.HostedServices
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
            var categoryRepository = provider.ServiceProvider.GetRequiredService<ICategoryRepository>();
            var productRepository = provider.ServiceProvider.GetRequiredService<IProductRepository>();
            var unitOfWork = provider.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var existing = await categoryRepository.GetAllAsync(cancellationToken);
            if (existing.Any())
                return;

            var categories = new List<Category>();
            foreach (var category in CatalogSeedData.Categories)
            {
                var entity = new Category(category.Name, category.Description);
                categoryRepository.Add(entity);
                categories.Add(entity);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var product = CatalogSeedData.Product;
            var productCategory = categories.First(x => x.Name == product.CategoryName);

            productRepository.Add(new Product(
                product.Name,
                product.Description,
                product.Code,
                product.Care,
                product.Price,
                productCategory.Id));

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}