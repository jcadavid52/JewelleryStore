using JewelleryStore.Modules.Inventory.Application.EntryPorts;
using JewelleryStore.Modules.Inventory.Application.UseCases.ReserveStockItem;
using JewelleryStore.Modules.Inventory.Contracts;

namespace JewelleryStore.Modules.Inventory.Infrastructure.EntryPointAdapters.InProcess;

public sealed class ReserveStockItemServiceAdapter : IReserveStockItemService
{
    private readonly IReserveStockItemUseCase _reserveStockItemUseCase;

    public ReserveStockItemServiceAdapter(IReserveStockItemUseCase reserveStockItemUseCase)
    {
        _reserveStockItemUseCase = reserveStockItemUseCase ?? throw new ArgumentNullException(nameof(reserveStockItemUseCase));
    }

    public async Task<ReserveStockItemResult> ReserveAsync(
        ReserveStockItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await _reserveStockItemUseCase.HandleAsync(
            new ReserveStockItemRequestDto(request.ProductId, request.Quantity),
            cancellationToken);

        return new ReserveStockItemResult(
            response.StockItemId,
            response.ProductId,
            response.ReservedQuantity,
            response.AvailableQuantity,
            response.Status);
    }
}