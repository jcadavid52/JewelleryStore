using JewelleryStore.Modules.Inventory.Application.Dtos;
using JewelleryStore.Modules.Inventory.Application.EntryPorts;
using JewelleryStore.Modules.Inventory.Domain.OuputPorts;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.GetAllStockItem
{
    public class GetAllStockItemHandler : IGetAllStockItemUseCase
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetAllStockItemHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository ?? throw new ArgumentNullException(nameof(stockItemRepository));
        }

        public async Task<GetAllStockItemResponseDto> HandleAsync(
            GetAllStockItemQueryDto query,
            CancellationToken cancellationToken = default)
        {
            var (stockItems, totalCount) = await _stockItemRepository.GetAllWithFiltersAsync(
                query.SearchTerm,
                query.PageNumber,
                query.PageSize,
                cancellationToken);

            var stockItemDtos = stockItems.Select(stockItem => new StockItemDto(
                stockItem.Id,
                stockItem.ProductId,
                stockItem.Available.Value,
                stockItem.Reserved.Value));

            return new GetAllStockItemResponseDto(
                stockItemDtos,
                totalCount,
                query.PageNumber,
                query.PageSize);
        }
    }
}
