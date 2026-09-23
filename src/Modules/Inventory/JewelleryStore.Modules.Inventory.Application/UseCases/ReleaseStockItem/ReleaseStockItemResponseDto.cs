using JewelleryStore.Modules.Inventory.Contracts;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.ReleaseStockItem;

public record ReleaseStockItemResponseDto(
    Guid? StockItemId,
    Guid ProductId,
    int ReleasedQuantity,
    int AvailableQuantity,
    ReleaseStockStatus Status);