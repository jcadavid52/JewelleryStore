namespace JewelleryStore.Modules.Orders.Application.Services;

public record StockConfirmationRequest(
    Guid ProductId,
    int Quantity);