using JewelleryStore.Modules.Checkout.Services;

namespace JewelleryStore.Modules.Checkout.UseCases.CreatePaymentIntent
{
    public class CreatePaymentIntentHandler : ICreatePaymentIntentUseCase
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentGateway _paymentGateway;

        public CreatePaymentIntentHandler(IOrderService orderService, IPaymentGateway paymentGateway)
        {
            _orderService = orderService;
            _paymentGateway = paymentGateway;
        }

        public async Task<CreatePaymentIntentResponseDto> HandleAsync(CreatePaymentIntentRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _orderService.CreateAsync(request, cancellationToken);

            var paymentResult = await _paymentGateway.ProcessAsync(new PaymentRequest(result.Id, result.Total), cancellationToken);

            return new CreatePaymentIntentResponseDto(paymentResult.ClientSecret, paymentResult.TransactionId);
        }
    }
}
