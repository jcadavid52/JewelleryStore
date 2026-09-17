using JewelleryStore.Modules.Inventory.Application.Dtos;
using JewelleryStore.Modules.Inventory.Application.EntryPorts;
using JewelleryStore.Modules.Inventory.Domain.Exceptions;
using JewelleryStore.Modules.Inventory.Domain.OuputPorts;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.GetStockItemById
{
    public class GetStockItemByIdHandler : IGetStockItemByIdUseCase
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetStockItemByIdHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository ?? throw new ArgumentNullException(nameof(stockItemRepository));
        }

        public async Task<StockItemDto> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var stockItem = await _stockItemRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new StockItemNotFoundException(id);

            return new StockItemDto(
                stockItem.Id,
                stockItem.ProductId,
                stockItem.Available.Value,
                stockItem.Reserved.Value);
        }
    }
}
