using JewelleryStore.Modules.Catalog.Contracts;
using JewelleryStore.Modules.Orders.Application.Services;

namespace JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.InProcess;

public sealed class CatalogPricingServiceAdapter : ICatalogPricingService
{
    private readonly IGetProductsByIdsService _getProductsByIdsService;

    public CatalogPricingServiceAdapter(IGetProductsByIdsService getProductsByIdsService)
    {
        _getProductsByIdsService = getProductsByIdsService ?? throw new ArgumentNullException(nameof(getProductsByIdsService));
    }

    public async Task<IReadOnlyCollection<CatalogItemInfo>> GetByIdsAsync(
        IReadOnlyCollection<Guid> productIds,
        CancellationToken cancellationToken = default)
    {
        var products = await _getProductsByIdsService.GetByIdsAsync(productIds, cancellationToken);

        return products
            .Select(product => new CatalogItemInfo(
                product.Id,
                product.Name,
                product.Price))
            .ToArray();
    }
}