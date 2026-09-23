using Microsoft.AspNetCore.Mvc;
using JewelleryStore.Modules.Orders.Application.EntryPorts;
using JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

namespace JewelleryStore.Modules.Orders.Infrastructure.EntryPointAdapters.Rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ICreateOrderUseCase _createOrderUseCase;

    public OrdersController(ICreateOrderUseCase createOrderUseCase)
    {
        _createOrderUseCase = createOrderUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _createOrderUseCase.HandleAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}