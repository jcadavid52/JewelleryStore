using FluentValidation;
using JewelleryStore.Modules.Catalog.Application.Dtos;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.GetProductsByIds
{
    public class GetProductsByIdsHandler : IGetProductsByIdsUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IValidator<GetProductsByIdsQueryDto> _validator;

        public GetProductsByIdsHandler(
            IProductRepository productRepository,
            IValidator<GetProductsByIdsQueryDto> validator)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task<GetProductsByIdsResponseDto> HandleAsync(
            GetProductsByIdsQueryDto query,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(query, cancellationToken);
            if (!validationResult.IsValid)
                throw validationResult.ToRequestValidationException();

            var products = await _productRepository.GetByIdsAsync(query.ProductIds, cancellationToken);

            var pricingItems = products
                .Select(product => new ProductPricingDto(
                    product.Id,
                    product.Name,
                    product.Price))
                .ToArray();

            return new GetProductsByIdsResponseDto(pricingItems);
        }
    }
}