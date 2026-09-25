using FluentValidation;
using JewelleryStore.Modules.Orders.Application.EntryPorts;
using JewelleryStore.Modules.Orders.Domain.Exceptions;
using JewelleryStore.Modules.Orders.Domain.OuputPorts;
using Microsoft.Extensions.Logging;

namespace JewelleryStore.Modules.Orders.Application.UseCases.ConfirmOrder;

public class ConfirmOrderHandler : IConfirmOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<ConfirmOrderRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmOrderHandler(
        IOrderRepository orderRepository,
        IValidator<ConfirmOrderRequestDto> validator,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ConfirmOrderResponseDto> HandleAsync(ConfirmOrderRequestDto request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken)
            ?? throw new OrderNotFoundException(request.OrderId);

        order.Confirm();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ConfirmOrderResponseDto(order.Id, order.OrderStatus);
    }
}