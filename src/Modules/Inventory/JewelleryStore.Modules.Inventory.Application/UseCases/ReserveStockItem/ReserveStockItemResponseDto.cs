using JewelleryStore.Modules.Inventory.Contracts;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.ReserveStockItem;

public record ReserveStockItemResponseDto(
    Guid? StockItemId,
    Guid ProductId,
    int ReservedQuantity,
    int AvailableQuantity,
    ReserveStockStatus Status);