using JewelleryStore.Modules.Orders.Domain.Enums;

namespace JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

public record CreateOrderResponseDto(
    Guid Id,
    Guid CustomerId,
    OrderStatus OrderStatus,
    decimal Total,
    IReadOnlyCollection<CreateOrderItemResponseDto> Items);