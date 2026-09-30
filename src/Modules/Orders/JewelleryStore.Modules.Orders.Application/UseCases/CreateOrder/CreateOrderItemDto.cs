namespace JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

public record CreateOrderItemDto(
    Guid ProductId,
    int Quantity);