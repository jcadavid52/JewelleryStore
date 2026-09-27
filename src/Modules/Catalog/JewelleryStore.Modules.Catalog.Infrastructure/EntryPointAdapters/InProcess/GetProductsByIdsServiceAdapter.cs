using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Application.UseCases.GetProductsByIds;
using JewelleryStore.Modules.Catalog.Contracts;

namespace JewelleryStore.Modules.Catalog.Infrastructure.EntryPointAdapters.InProcess;

public sealed class GetProductsByIdsServiceAdapter : IGetProductsByIdsService
{
    private readonly IGetProductsByIdsUseCase _getProductsByIdsUseCase;

    public GetProductsByIdsServiceAdapter(IGetProductsByIdsUseCase getProductsByIdsUseCase)
    {
        _getProductsByIdsUseCase = getProductsByIdsUseCase ?? throw new ArgumentNullException(nameof(getProductsByIdsUseCase));
    }

    public async Task<IReadOnlyCollection<CatalogProductInfo>> GetByIdsAsync(
        IReadOnlyCollection<Guid> productIds,
        CancellationToken cancellationToken = default)
    {
        var result = await _getProductsByIdsUseCase.HandleAsync(
            new GetProductsByIdsQueryDto(productIds),
            cancellationToken);

        return result.Products
            .Select(product => new CatalogProductInfo(
                product.Id,
                product.Name,
                product.Price))
            .ToArray();
    }
}