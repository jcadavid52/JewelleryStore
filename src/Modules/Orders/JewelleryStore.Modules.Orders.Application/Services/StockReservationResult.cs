namespace JewelleryStore.Modules.Orders.Application.Services;

public record StockReservationResult(
    Guid ProductId,
    int ReservedQuantity,
    int AvailableQuantity,
    StockReservationStatus Status);