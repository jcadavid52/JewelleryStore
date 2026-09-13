using JewelleryStore.Modules.Catalog.Application.Dtos;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCategory
{
    public class GetAllCategoryHandler : IGetAllCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<GetAllCategoryResponseDto> HandleAsync(
            GetAllCategoryQueryDto query,
            CancellationToken cancellationToken = default)
        {
            var (categories, totalCount) = await _categoryRepository.GetAllWithFiltersAsync(
                query.SearchTerm,
                query.PageNumber,
                query.PageSize,
                cancellationToken);

            var categoryDtos = categories.Select(category => new CategoryDto(
                category.Id,
                category.Name,
                category.Description));

            return new GetAllCategoryResponseDto(
                categoryDtos,
                totalCount,
                query.PageNumber,
                query.PageSize);
        }
    }
}