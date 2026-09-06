using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Domain.Entities;
using JewelleryStore.Modules.Catalog.Domain.Exceptions;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.CreateProduct;

public class CreateProductHandler : ICreateProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<CreateProductResponseDto> HandleAsync(CreateProductRequestDto request)
    {
        if (await _productRepository.ExistsByNameAsync(request.Name))
            throw new ProductNameAlreadyExistsException(request.Name);

        if (await _productRepository.ExistsByCodeAsync(request.Code))
            throw new ProductCodeAlreadyExistsException(request.Code);

        if (!await _categoryRepository.ExistsAsync(request.CategoryId))
            throw new ProductCategoryNotFoundException(request.CategoryId);

        var product = new Product(
            request.Name,
            request.Description,
            request.Code,
            request.Care,
            request.Price,
            request.CategoryId);

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync();

        return new CreateProductResponseDto(
            product.Id,
            product.Name,
            product.Description,
            product.Code,
            product.Care,
            product.Price,
            product.CategoryId);
    }
}
