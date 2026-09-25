namespace JewelleryStore.Modules.Orders.Contracts;

public record ConfirmOrderRequest(
    Guid OrderId);