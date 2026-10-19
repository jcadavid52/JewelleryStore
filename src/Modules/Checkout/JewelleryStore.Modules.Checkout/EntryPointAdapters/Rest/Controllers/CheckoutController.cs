using Microsoft.AspNetCore.Mvc;
using JewelleryStore.Modules.Checkout.UseCases;
using JewelleryStore.Modules.Checkout.UseCases.Checkout;
using JewelleryStore.Modules.Checkout.UseCases.CreatePaymentIntent;

namespace JewelleryStore.Modules.Checkout.EntryPointAdapters.Rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly ICheckoutUseCase _checkoutUseCase;
    private readonly ICreatePaymentIntentUseCase _createPaymentIntentUseCase;

    public CheckoutController(ICheckoutUseCase checkoutUseCase, ICreatePaymentIntentUseCase createPaymentIntentUseCase)
    {
        _checkoutUseCase = checkoutUseCase ?? throw new ArgumentNullException(nameof(checkoutUseCase));
        _createPaymentIntentUseCase = createPaymentIntentUseCase ?? throw new ArgumentNullException(nameof(createPaymentIntentUseCase));
    }

    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _checkoutUseCase.HandleAsync(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("create-intent")]
    public async Task<IActionResult> CreatePaymentIntent([FromBody] CreatePaymentIntentRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _createPaymentIntentUseCase.HandleAsync(request, cancellationToken);
        return Ok(result);
    }
}