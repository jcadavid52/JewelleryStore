using JewelleryStore.Modules.Catalog.Application.Dtos;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCategory
{
    public record GetAllCategoryResponseDto
    {
        public IEnumerable<CategoryDto> Categories { get; init; }
        public int TotalCount { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public bool HasPreviousPage { get; init; }
        public bool HasNextPage { get; init; }

        public GetAllCategoryResponseDto(
            IEnumerable<CategoryDto> categories,
            int totalCount,
            int pageNumber,
            int pageSize)
        {
            Categories = categories;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            HasPreviousPage = pageNumber > 1;
            HasNextPage = pageNumber * pageSize < totalCount;
        }
    }
}