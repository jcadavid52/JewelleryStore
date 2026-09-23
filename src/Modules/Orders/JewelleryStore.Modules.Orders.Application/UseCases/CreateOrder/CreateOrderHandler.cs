using FluentValidation;
using JewelleryStore.Modules.Orders.Application.EntryPorts;
using JewelleryStore.Modules.Orders.Application.Services;
using JewelleryStore.Modules.Orders.Domain.Entities;
using JewelleryStore.Modules.Orders.Domain.Exceptions;
using JewelleryStore.Modules.Orders.Domain.OuputPorts;
using JewelleryStore.Modules.Orders.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

public class CreateOrderHandler : ICreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IStockReservationService _stockReservationService;
    private readonly IValidator<CreateOrderRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateOrderHandler> _logger;

    public CreateOrderHandler(
        IOrderRepository orderRepository,
        IStockReservationService stockReservationService,
        IValidator<CreateOrderRequestDto> validator,
        IUnitOfWork unitOfWork,
        ILogger<CreateOrderHandler> logger)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _stockReservationService = stockReservationService ?? throw new ArgumentNullException(nameof(stockReservationService));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CreateOrderResponseDto> HandleAsync(CreateOrderRequestDto request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var reservedItems = new List<CreateOrderItemDto>();

        try
        {
            foreach (var item in request.Items)
            {
                var reservation = await _stockReservationService.ReserveAsync(
                    new StockReservationRequest(item.ProductId, item.Quantity),
                    cancellationToken);

                if (reservation.Status != StockReservationStatus.Reserved)
                    throw new StockReservationRejectedException(
                        item.ProductId,
                        item.Quantity,
                        reservation.AvailableQuantity);

                reservedItems.Add(item);
            }

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
        catch
        {
            await CompensateAsync(reservedItems, cancellationToken);
            throw;
        }
    }

    private async Task CompensateAsync(IReadOnlyCollection<CreateOrderItemDto> reservedItems, CancellationToken cancellationToken)
    {
        foreach (var item in reservedItems)
        {
            try
            {
                await _stockReservationService.ReleaseAsync(
                    new StockReleaseRequest(item.ProductId, item.Quantity),
                    cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception,
                    "Falló la compensación de la reserva para el producto {ProductId} x {Quantity}",
                    item.ProductId,
                    item.Quantity);
            }
        }
    }
}