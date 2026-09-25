namespace JewelleryStore.Modules.Orders.Application.UseCases.ConfirmOrder;

public record ConfirmOrderRequestDto(
    Guid OrderId);