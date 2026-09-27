using FluentValidation;
using JewelleryStore.Modules.Orders.Application.EntryPorts;
using JewelleryStore.Modules.Orders.Application.Services;
using JewelleryStore.Modules.Orders.Domain.Exceptions;
using JewelleryStore.Modules.Orders.Domain.OuputPorts;
using Microsoft.Extensions.Logging;

namespace JewelleryStore.Modules.Orders.Application.UseCases.CancelOrder;

public class CancelOrderHandler : ICancelOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IStockReservationService _stockReservationService;
    private readonly IValidator<CancelOrderRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;
    public CancelOrderHandler(
        IOrderRepository orderRepository,
        IStockReservationService stockReservationService,
        IValidator<CancelOrderRequestDto> validator,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _stockReservationService = stockReservationService ?? throw new ArgumentNullException(nameof(stockReservationService));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<CancelOrderResponseDto> HandleAsync(CancelOrderRequestDto request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken)
            ?? throw new OrderNotFoundException(request.OrderId);

        order.Cancel();

        foreach (var item in order.Detalles)
        {
            await _stockReservationService.ReleaseAsync(
                new StockReleaseRequest(item.ProductId, item.Quantity),
                cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CancelOrderResponseDto(order.Id, order.OrderStatus);
    }
}