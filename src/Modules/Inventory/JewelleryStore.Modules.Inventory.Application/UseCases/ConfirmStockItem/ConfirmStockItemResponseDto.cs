using JewelleryStore.Modules.Inventory.Contracts;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.ConfirmStockItem;

public record ConfirmStockItemResponseDto(
    Guid? StockItemId,
    Guid ProductId,
    int ConfirmedQuantity,
    int ReservedQuantity,
    ConfirmStockStatus Status);