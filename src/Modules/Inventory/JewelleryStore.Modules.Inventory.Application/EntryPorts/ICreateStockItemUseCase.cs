using JewelleryStore.Modules.Inventory.Application.UseCases.CreateStockItem;

namespace JewelleryStore.Modules.Inventory.Application.EntryPorts;

public interface ICreateStockItemUseCase
{
    Task<CreateStockItemResponseDto> HandleAsync(CreateStockItemRequestDto request, CancellationToken cancellationToken = default);
}
