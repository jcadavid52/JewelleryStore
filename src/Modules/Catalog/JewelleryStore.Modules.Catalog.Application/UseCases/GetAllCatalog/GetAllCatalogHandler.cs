using JewelleryStore.Modules.Catalog.Application.Dtos;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCatalog
{
    public class GetAllCatalogHandler : IGetAllCatalogUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetAllCatalogHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<GetAllCatalogResponseDto> HandleAsync(
            GetAllCatalogQueryDto query,
            CancellationToken cancellationToken = default)
        {
            var (products, totalCount) = await _productRepository.GetAllWithFiltersAsync(
                query.SearchTerm,
                query.CategoryId,
                query.PageNumber,
                query.PageSize,
                cancellationToken);

            var productDtos = products.Select(product => new ProductDto(
                product.Id,
                product.Name,
                product.Description,
                product.Code,
                product.Care,
                product.Price,
                new CategoryDto(
                    product.Category!.Id,
                    product.Category.Name,
                    product.Category.Description)));

            return new GetAllCatalogResponseDto(
                productDtos,
                totalCount,
                query.PageNumber,
                query.PageSize);
        }
    }
}