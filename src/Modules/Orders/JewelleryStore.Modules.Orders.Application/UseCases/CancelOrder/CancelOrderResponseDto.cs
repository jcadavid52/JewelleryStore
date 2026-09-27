using JewelleryStore.Modules.Orders.Domain.Enums;

namespace JewelleryStore.Modules.Orders.Application.UseCases.CancelOrder;

public record CancelOrderResponseDto(
    Guid Id,
    OrderStatus OrderStatus);