using JewelleryStore.Modules.Inventory.Application.UseCases.GetAllStockItem;

namespace JewelleryStore.Modules.Inventory.Application.EntryPorts;

public interface IGetAllStockItemUseCase
{
    Task<GetAllStockItemResponseDto> HandleAsync(GetAllStockItemQueryDto query, CancellationToken cancellationToken = default);
}
