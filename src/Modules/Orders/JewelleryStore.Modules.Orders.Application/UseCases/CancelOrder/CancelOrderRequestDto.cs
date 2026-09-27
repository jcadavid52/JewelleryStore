namespace JewelleryStore.Modules.Orders.Application.UseCases.CancelOrder;

public record CancelOrderRequestDto(
    Guid OrderId);