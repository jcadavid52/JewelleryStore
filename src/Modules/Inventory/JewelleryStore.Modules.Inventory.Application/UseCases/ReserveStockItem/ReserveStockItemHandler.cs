using FluentValidation;
using JewelleryStore.Modules.Inventory.Application.EntryPorts;
using JewelleryStore.Modules.Inventory.Contracts;
using JewelleryStore.Modules.Inventory.Domain.Exceptions;
using JewelleryStore.Modules.Inventory.Domain.OuputPorts;
using JewelleryStore.Modules.Inventory.Domain.ValueObjects;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.ReserveStockItem;

public class ReserveStockItemHandler : IReserveStockItemUseCase
{
    private readonly IStockItemRepository _stockItemRepository;
    private readonly IValidator<ReserveStockItemRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public ReserveStockItemHandler(
        IStockItemRepository stockItemRepository,
        IValidator<ReserveStockItemRequestDto> validator,
        IUnitOfWork unitOfWork)
    {
        _stockItemRepository = stockItemRepository ?? throw new ArgumentNullException(nameof(stockItemRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ReserveStockItemResponseDto> HandleAsync(
        ReserveStockItemRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var stockItem = await _stockItemRepository.GetByProductIdAsync(request.ProductId, cancellationToken);
        if (stockItem is null)
        {
            return new ReserveStockItemResponseDto(
                StockItemId: null,
                ProductId: request.ProductId,
                ReservedQuantity: 0,
                AvailableQuantity: 0,
                Status: ReserveStockStatus.StockItemNotFound);
        }

        try
        {
            stockItem.ReserveStock(new Quantity(request.Quantity));
        }
        catch (InsufficientStockException exception)
        {
            return new ReserveStockItemResponseDto(
                StockItemId: stockItem.Id,
                ProductId: request.ProductId,
                ReservedQuantity: 0,
                AvailableQuantity: exception.Available,
                Status: ReserveStockStatus.InsufficientStock);
        }

        _stockItemRepository.Update(stockItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ReserveStockItemResponseDto(
            StockItemId: stockItem.Id,
            ProductId: request.ProductId,
            ReservedQuantity: request.Quantity,
            AvailableQuantity: stockItem.Available.Value,
            Status: ReserveStockStatus.Reserved);
    }
}