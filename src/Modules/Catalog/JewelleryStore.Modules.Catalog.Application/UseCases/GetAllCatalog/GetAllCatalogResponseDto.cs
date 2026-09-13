using JewelleryStore.Modules.Catalog.Application.Dtos;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCatalog
{
    public record GetAllCatalogResponseDto
    {
        public IEnumerable<ProductDto> Products { get; init; }
        public int TotalCount { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public bool HasPreviousPage { get; init; }
        public bool HasNextPage  { get; init; }

        public GetAllCatalogResponseDto(
            IEnumerable<ProductDto> products,
            int totalCount,
            int pageNumber,
            int pageSize
            )
        {
            Products = products;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            HasPreviousPage = pageNumber > 1;
            HasNextPage = pageNumber * pageSize < totalCount;
        }
    }
}
