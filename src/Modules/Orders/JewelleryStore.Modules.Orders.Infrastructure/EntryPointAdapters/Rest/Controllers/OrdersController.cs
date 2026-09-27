using Microsoft.AspNetCore.Mvc;
using JewelleryStore.Modules.Orders.Application.EntryPorts;
using JewelleryStore.Modules.Orders.Application.UseCases.CancelOrder;
using JewelleryStore.Modules.Orders.Application.UseCases.ConfirmOrder;
using JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

namespace JewelleryStore.Modules.Orders.Infrastructure.EntryPointAdapters.Rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ICreateOrderUseCase _createOrderUseCase;
    private readonly IConfirmOrderUseCase _confirmOrderUseCase;
    private readonly ICancelOrderUseCase _cancelOrderUseCase;

    public OrdersController(
        ICreateOrderUseCase createOrderUseCase,
        IConfirmOrderUseCase confirmOrderUseCase,
        ICancelOrderUseCase cancelOrderUseCase)
    {
        _createOrderUseCase = createOrderUseCase;
        _confirmOrderUseCase = confirmOrderUseCase;
        _cancelOrderUseCase = cancelOrderUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _createOrderUseCase.HandleAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}/confirm")]
    public async Task<IActionResult> Confirm(Guid id, CancellationToken cancellationToken)
    {
        var result = await _confirmOrderUseCase.HandleAsync(new ConfirmOrderRequestDto(id), cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        var result = await _cancelOrderUseCase.HandleAsync(new CancelOrderRequestDto(id), cancellationToken);
        return Ok(result);
    }
}