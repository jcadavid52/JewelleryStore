using FluentValidation;
using JewelleryStore.Modules.Checkout.Services;
using JewelleryStore.Modules.Orders.Contracts;
using Microsoft.Extensions.Logging;

namespace JewelleryStore.Modules.Checkout.UseCases.Checkout;

public class CheckoutHandler : ICheckoutUseCase
{
    private readonly ICreateOrderService _createOrderService;
    private readonly IConfirmOrderService _confirmOrderService;
    private readonly ICancelOrderService _cancelOrderService;
    private readonly IPaymentGateway _paymentGateway;
    private readonly IValidator<CheckoutRequestDto> _validator;
    private readonly ILogger<CheckoutHandler> _logger;

    public CheckoutHandler(
        ICreateOrderService createOrderService,
        IConfirmOrderService confirmOrderService,
        ICancelOrderService cancelOrderService,
        IPaymentGateway paymentGateway,
        IValidator<CheckoutRequestDto> validator,
        ILogger<CheckoutHandler> logger)
    {
        _createOrderService = createOrderService ?? throw new ArgumentNullException(nameof(createOrderService));
        _confirmOrderService = confirmOrderService ?? throw new ArgumentNullException(nameof(confirmOrderService));
        _cancelOrderService = cancelOrderService ?? throw new ArgumentNullException(nameof(cancelOrderService));
        _paymentGateway = paymentGateway ?? throw new ArgumentNullException(nameof(paymentGateway));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CheckoutResponseDto> HandleAsync(
        CheckoutRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var order = await _createOrderService.CreateAsync(
            new CreateOrderRequest(
                request.CustomerId,
                new CreateOrderShippingAddressRequest(
                    request.ShippingAddress.Address,
                    request.ShippingAddress.City,
                    request.ShippingAddress.PostalCode,
                    request.ShippingAddress.Phone),
                request.Items
                    .Select(item => new CreateOrderRequestItem(item.ProductId, item.Quantity))
                    .ToArray()),
            cancellationToken);

        var payment = await _paymentGateway.ProcessAsync(
            new PaymentRequest(order.Id, order.Total),
            cancellationToken);

        if (payment.Status == PaymentStatus.Succeeded)
        {
            await _confirmOrderService.ConfirmAsync(
                new ConfirmOrderRequest(order.Id),
                cancellationToken);

            _logger.LogInformation(
                "Checkout completado para el pedido {OrderId}, total {Total}",
                order.Id,
                order.Total);

            return new CheckoutResponseDto(
                order.Id,
                "Confirmed",
                PaymentStatus.Succeeded,
                order.Total);
        }

        await _cancelOrderService.CancelAsync(
            new CancelOrderRequest(order.Id),
            cancellationToken);

        _logger.LogInformation(
            "El pago del pedido {OrderId} fallo ({Detail}); se cancelo para liberar stock",
            order.Id,
            payment.Detail);

        return new CheckoutResponseDto(
            order.Id,
            "Cancelled",
            PaymentStatus.Failed,
            order.Total);
    }
}