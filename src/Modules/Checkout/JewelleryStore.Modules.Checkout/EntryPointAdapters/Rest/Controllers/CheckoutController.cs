using Microsoft.AspNetCore.Mvc;
using JewelleryStore.Modules.Checkout.UseCases;
using JewelleryStore.Modules.Checkout.UseCases.Checkout;

namespace JewelleryStore.Modules.Checkout.EntryPointAdapters.Rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckoutController : ControllerBase
{
    private readonly ICheckoutUseCase _checkoutUseCase;

    public CheckoutController(ICheckoutUseCase checkoutUseCase)
    {
        _checkoutUseCase = checkoutUseCase ?? throw new ArgumentNullException(nameof(checkoutUseCase));
    }

    [HttpPost]
    public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _checkoutUseCase.HandleAsync(request, cancellationToken);
        return Ok(result);
    }
}