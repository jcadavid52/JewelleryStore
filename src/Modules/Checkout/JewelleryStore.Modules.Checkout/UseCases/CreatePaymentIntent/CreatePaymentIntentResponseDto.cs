namespace JewelleryStore.Modules.Checkout.UseCases.CreatePaymentIntent
{
    public record CreatePaymentIntentResponseDto(
        string ClientSecret,
        string PaymentIntentId);
}
