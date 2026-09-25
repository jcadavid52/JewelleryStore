using JewelleryStore.Modules.Orders.Domain.Enums;

namespace JewelleryStore.Modules.Orders.Application.UseCases.ConfirmOrder;

public record ConfirmOrderResponseDto(
    Guid Id,
    OrderStatus OrderStatus);