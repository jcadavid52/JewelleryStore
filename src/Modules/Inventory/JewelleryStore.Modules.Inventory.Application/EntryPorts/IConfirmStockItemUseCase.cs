using JewelleryStore.Modules.Inventory.Application.UseCases.ConfirmStockItem;

namespace JewelleryStore.Modules.Inventory.Application.EntryPorts;

public interface IConfirmStockItemUseCase
{
    Task<ConfirmStockItemResponseDto> HandleAsync(ConfirmStockItemRequestDto request, CancellationToken cancellationToken = default);
}