using JewelleryStore.Modules.Inventory.Application.EntryPorts;
using JewelleryStore.Modules.Inventory.Application.UseCases.ReleaseStockItem;
using JewelleryStore.Modules.Inventory.Contracts;

namespace JewelleryStore.Modules.Inventory.Infrastructure.EntryPointAdapters.InProcess;

public sealed class ReleaseStockItemServiceAdapter : IReleaseStockItemService
{
    private readonly IReleaseStockItemUseCase _releaseStockItemUseCase;

    public ReleaseStockItemServiceAdapter(IReleaseStockItemUseCase releaseStockItemUseCase)
    {
        _releaseStockItemUseCase = releaseStockItemUseCase ?? throw new ArgumentNullException(nameof(releaseStockItemUseCase));
    }

    public async Task<ReleaseStockItemResult> ReleaseAsync(
        ReleaseStockItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await _releaseStockItemUseCase.HandleAsync(
            new ReleaseStockItemRequestDto(request.ProductId, request.Quantity),
            cancellationToken);

        return new ReleaseStockItemResult(
            response.StockItemId,
            response.ProductId,
            response.ReleasedQuantity,
            response.AvailableQuantity,
            response.Status);
    }
}