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
    private readonly ICatalogPricingService _catalogPricingService;
    private readonly IValidator<CreateOrderRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateOrderHandler> _logger;

    public CreateOrderHandler(
        IOrderRepository orderRepository,
        IStockReservationService stockReservationService,
        ICatalogPricingService catalogPricingService,
        IValidator<CreateOrderRequestDto> validator,
        IUnitOfWork unitOfWork,
        ILogger<CreateOrderHandler> logger)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _stockReservationService = stockReservationService ?? throw new ArgumentNullException(nameof(stockReservationService));
        _catalogPricingService = catalogPricingService ?? throw new ArgumentNullException(nameof(catalogPricingService));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CreateOrderResponseDto> HandleAsync(CreateOrderRequestDto request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var unitPrices = await ResolveCatalogPricesAsync(request, cancellationToken);

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
                order.AddOrderItem(item.ProductId, item.Quantity, unitPrices[item.ProductId]);

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

    private async Task<IReadOnlyDictionary<Guid, decimal>> ResolveCatalogPricesAsync(
        CreateOrderRequestDto request,
        CancellationToken cancellationToken)
    {
        var productIds = request.Items
            .Select(item => item.ProductId)
            .Distinct()
            .ToArray();

        var catalogItems = await _catalogPricingService.GetByIdsAsync(productIds, cancellationToken);

        var foundProductIds = catalogItems
            .Select(item => item.ProductId)
            .ToHashSet();

        var missingProductIds = productIds
            .Where(id => !foundProductIds.Contains(id))
            .ToArray();

        if (missingProductIds.Length > 0)
            throw new CatalogProductNotFoundException(missingProductIds);

        return catalogItems.ToDictionary(
            item => item.ProductId,
            item => item.UnitPrice);
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
                    "Fallo la compensaci�n de la reserva para el producto {ProductId} x {Quantity}",
                    item.ProductId,
                    item.Quantity);
            }
        }
    }
}