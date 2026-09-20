using FluentValidation;
using JewelleryStore.Modules.Orders.Application.EntryPorts;
using JewelleryStore.Modules.Orders.Domain.Entities;
using JewelleryStore.Modules.Orders.Domain.OuputPorts;
using JewelleryStore.Modules.Orders.Domain.ValueObjects;

namespace JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

public class CreateOrderHandler : ICreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IValidator<CreateOrderRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderHandler(
        IOrderRepository orderRepository,
        IValidator<CreateOrderRequestDto> validator,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<CreateOrderResponseDto> HandleAsync(CreateOrderRequestDto request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var order = new Order(
            request.CustomerId,
            new ShippingAddress(
                request.ShippingAddress.Address,
                request.ShippingAddress.City,
                request.ShippingAddress.PostalCode,
                request.ShippingAddress.Phone));

        foreach (var item in request.Items)
            order.AddOrderItem(item.ProductId, item.Quantity, item.UnitPrice);

        _orderRepository.Add(order);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var items = order.Detalles
            .Select(item => new CreateOrderItemResponseDto(
                item.ProductId,
                item.Quantity,
                item.UnitPrice,
                item.SubTotal))
            .ToArray();

        return new CreateOrderResponseDto(
            order.Id,
            order.CustomerId,
            order.OrderStatus,
            order.Total,
            items);
    }
}