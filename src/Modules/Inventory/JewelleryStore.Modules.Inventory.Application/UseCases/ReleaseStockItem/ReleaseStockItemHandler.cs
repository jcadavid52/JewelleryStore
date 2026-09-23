using FluentValidation;
using JewelleryStore.Modules.Inventory.Application.EntryPorts;
using JewelleryStore.Modules.Inventory.Contracts;
using JewelleryStore.Modules.Inventory.Domain.Exceptions;
using JewelleryStore.Modules.Inventory.Domain.OuputPorts;
using JewelleryStore.Modules.Inventory.Domain.ValueObjects;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.ReleaseStockItem;

public class ReleaseStockItemHandler : IReleaseStockItemUseCase
{
    private readonly IStockItemRepository _stockItemRepository;
    private readonly IValidator<ReleaseStockItemRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public ReleaseStockItemHandler(
        IStockItemRepository stockItemRepository,
        IValidator<ReleaseStockItemRequestDto> validator,
        IUnitOfWork unitOfWork)
    {
        _stockItemRepository = stockItemRepository ?? throw new ArgumentNullException(nameof(stockItemRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ReleaseStockItemResponseDto> HandleAsync(
        ReleaseStockItemRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var stockItem = await _stockItemRepository.GetByProductIdAsync(request.ProductId, cancellationToken);
        if (stockItem is null)
        {
            return new ReleaseStockItemResponseDto(
                StockItemId: null,
                ProductId: request.ProductId,
                ReleasedQuantity: 0,
                AvailableQuantity: 0,
                Status: ReleaseStockStatus.StockItemNotFound);
        }

        try
        {
            stockItem.ReleaseReservation(new Quantity(request.Quantity));
        }
        catch (InsufficientReservedStockException)
        {
            return new ReleaseStockItemResponseDto(
                StockItemId: stockItem.Id,
                ProductId: request.ProductId,
                ReleasedQuantity: 0,
                AvailableQuantity: stockItem.Available.Value,
                Status: ReleaseStockStatus.InsufficientReserved);
        }

        _stockItemRepository.Update(stockItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ReleaseStockItemResponseDto(
            StockItemId: stockItem.Id,
            ProductId: request.ProductId,
            ReleasedQuantity: request.Quantity,
            AvailableQuantity: stockItem.Available.Value,
            Status: ReleaseStockStatus.Released);
    }
}