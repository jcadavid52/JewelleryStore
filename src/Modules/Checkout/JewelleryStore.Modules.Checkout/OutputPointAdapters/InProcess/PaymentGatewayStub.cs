using JewelleryStore.Modules.Checkout.Services;
using Microsoft.Extensions.Options;

namespace JewelleryStore.Modules.Checkout.OutputPointAdapters.InProcess;

public sealed class PaymentGatewayStub : IPaymentGateway
{
    private readonly PaymentOptions _options;

    public PaymentGatewayStub(IOptions<PaymentOptions> options)
    {
        _options = options.Value;
    }

    public Task<PaymentResult> ProcessAsync(
        PaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        if (_options.SimulateFailure)
        {
            return Task.FromResult(new PaymentResult(
                PaymentStatus.Failed,
                string.Empty,
                "Pago rechazado (simulaci�n)."));
        }

        return Task.FromResult(new PaymentResult(
            PaymentStatus.Succeeded,
            Guid.NewGuid().ToString("N"),
            "Pago aprobado."));
    }
}