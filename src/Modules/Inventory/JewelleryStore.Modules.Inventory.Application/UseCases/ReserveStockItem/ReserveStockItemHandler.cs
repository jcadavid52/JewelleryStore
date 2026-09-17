using FluentValidation;
using JewelleryStore.Modules.Inventory.Application.EntryPorts;
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

    public async Task HandleAsync(ReserveStockItemRequestDto request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var stockItem = await _stockItemRepository.GetByProductIdAsync(request.ProductId, cancellationToken)
            ?? throw new StockItemNotFoundException(request.ProductId);

        stockItem.ReserveStock(new Quantity(request.Quantity));

        _stockItemRepository.Update(stockItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
