using JewelleryStore.Modules.Inventory.Application.Dtos;

namespace JewelleryStore.Modules.Inventory.Application.EntryPorts;

public interface IGetStockItemByProductIdUseCase
{
    Task<StockItemDto> HandleAsync(Guid productId, CancellationToken cancellationToken = default);
}
