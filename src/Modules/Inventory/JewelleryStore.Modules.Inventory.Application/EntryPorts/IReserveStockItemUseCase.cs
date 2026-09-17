using JewelleryStore.Modules.Inventory.Application.UseCases.ReserveStockItem;

namespace JewelleryStore.Modules.Inventory.Application.EntryPorts;

public interface IReserveStockItemUseCase
{
    Task HandleAsync(ReserveStockItemRequestDto request, CancellationToken cancellationToken = default);
}
