namespace JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

public record CreateOrderItemResponseDto(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    decimal SubTotal);