namespace JewelleryStore.Modules.Checkout.UseCases.Checkout;

public record CheckoutItemDto(
    Guid ProductId,
    int Quantity);