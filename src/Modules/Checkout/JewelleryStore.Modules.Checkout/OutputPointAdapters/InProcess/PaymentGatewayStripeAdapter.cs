using JewelleryStore.Modules.Checkout.Exceptions;
using JewelleryStore.Modules.Checkout.Services;
using Stripe;

namespace JewelleryStore.Modules.Checkout.OutputPointAdapters.InProcess
{
    public class PaymentGatewayStripeAdapter : IPaymentGateway
    {
        public async Task<PaymentResult> ProcessAsync(PaymentRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var intent = await new PaymentIntentService().CreateAsync(
                new PaymentIntentCreateOptions
                {
                    Amount = (long)(request.Amount * 100),
                    Currency = "usd",
                    PaymentMethod = "pm_card_visa",
                    Description = $"Payment for order {request.OrderId}",
                    Metadata = new Dictionary<string, string>
                    {
                        { "order_id", request.OrderId.ToString() },
                        // {"customer_id", request.CustomerId.ToString() }
                    }
                },
                new RequestOptions
                {
                    IdempotencyKey = $"order-{request.OrderId}-intent"
                });

                return new PaymentResult(
                    PaymentStatus.Succeeded,
                    intent.Id,
                    intent.Description,
                    intent.ClientSecret);
            }
            catch (StripeException ex)
            {
                throw new CreatePaymentIntentException($"Stripe error for order {request.OrderId}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new CreatePaymentIntentException($"Payment processing failed for order {request.OrderId}: {ex.Message}");
            }
        }
    }
}
