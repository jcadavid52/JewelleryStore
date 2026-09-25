using JewelleryStore.Modules.Inventory.Application.EntryPorts;
using JewelleryStore.Modules.Inventory.Application.UseCases.ConfirmStockItem;
using JewelleryStore.Modules.Inventory.Contracts;

namespace JewelleryStore.Modules.Inventory.Infrastructure.EntryPointAdapters.InProcess;

public sealed class ConfirmStockItemServiceAdapter : IConfirmStockItemService
{
    private readonly IConfirmStockItemUseCase _confirmStockItemUseCase;

    public ConfirmStockItemServiceAdapter(IConfirmStockItemUseCase confirmStockItemUseCase)
    {
        _confirmStockItemUseCase = confirmStockItemUseCase ?? throw new ArgumentNullException(nameof(confirmStockItemUseCase));
    }

    public async Task<ConfirmStockItemResult> ConfirmAsync(
        ConfirmStockItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var response = await _confirmStockItemUseCase.HandleAsync(
            new ConfirmStockItemRequestDto(request.ProductId, request.Quantity),
            cancellationToken);

        return new ConfirmStockItemResult(
            response.StockItemId,
            response.ProductId,
            response.ConfirmedQuantity,
            response.ReservedQuantity,
            response.Status);
    }
}