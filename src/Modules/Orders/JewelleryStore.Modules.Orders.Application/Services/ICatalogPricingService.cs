namespace JewelleryStore.Modules.Orders.Application.Services;

public interface ICatalogPricingService
{
    Task<IReadOnlyCollection<CatalogItemInfo>> GetByIdsAsync(
        IReadOnlyCollection<Guid> productIds,
        CancellationToken cancellationToken = default);
}