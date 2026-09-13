using JewelleryStore.Modules.Catalog.Application.Dtos;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Domain.Exceptions;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.GetProductById
{
    public class GetProductByIdHandler : IGetProductByIdUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task<ProductDto> HandleAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByIdWithCategoryAsync(id, cancellationToken)
                ?? throw new ProductNotFoundException(id);

            return new ProductDto(
                product.Id,
                product.Name,
                product.Description,
                product.Code,
                product.Care,
                product.Price,
                new CategoryDto(
                    product.Category!.Id,
                    product.Category.Name,
                    product.Category.Description));
        }
    }
}