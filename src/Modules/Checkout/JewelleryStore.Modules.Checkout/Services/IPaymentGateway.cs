namespace JewelleryStore.Modules.Checkout.Services;

public interface IPaymentGateway
{
    Task<PaymentResult> ProcessAsync(
        PaymentRequest request,
        CancellationToken cancellationToken = default);
}