namespace JewelleryStore.Modules.Orders.Application.Services;

public record StockReservationRequest(
    Guid ProductId,
    int Quantity);