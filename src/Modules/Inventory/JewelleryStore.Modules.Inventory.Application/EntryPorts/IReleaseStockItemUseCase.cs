using JewelleryStore.Modules.Inventory.Application.UseCases.ReleaseStockItem;

namespace JewelleryStore.Modules.Inventory.Application.EntryPorts;

public interface IReleaseStockItemUseCase
{
    Task<ReleaseStockItemResponseDto> HandleAsync(ReleaseStockItemRequestDto request, CancellationToken cancellationToken = default);
}