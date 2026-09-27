namespace JewelleryStore.Modules.Catalog.Contracts;

public interface IGetProductsByIdsService
{
    Task<IReadOnlyCollection<CatalogProductInfo>> GetByIdsAsync(
        IReadOnlyCollection<Guid> productIds,
        CancellationToken cancellationToken = default);
}