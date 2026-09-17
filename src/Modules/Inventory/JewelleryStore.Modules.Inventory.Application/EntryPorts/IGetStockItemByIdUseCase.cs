using JewelleryStore.Modules.Inventory.Application.Dtos;

namespace JewelleryStore.Modules.Inventory.Application.EntryPorts;

public interface IGetStockItemByIdUseCase
{
    Task<StockItemDto> HandleAsync(Guid id, CancellationToken cancellationToken = default);
}
