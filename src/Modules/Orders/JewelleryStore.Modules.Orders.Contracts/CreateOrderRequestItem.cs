namespace JewelleryStore.Modules.Orders.Contracts;

public record CreateOrderRequestItem(
    Guid ProductId,
    int Quantity);