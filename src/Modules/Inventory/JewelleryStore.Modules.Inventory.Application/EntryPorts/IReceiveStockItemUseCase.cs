using JewelleryStore.Modules.Inventory.Application.UseCases.ReceiveStockItem;

namespace JewelleryStore.Modules.Inventory.Application.EntryPorts;

public interface IReceiveStockItemUseCase
{
    Task HandleAsync(ReceiveStockItemRequestDto request, CancellationToken cancellationToken = default);
}
