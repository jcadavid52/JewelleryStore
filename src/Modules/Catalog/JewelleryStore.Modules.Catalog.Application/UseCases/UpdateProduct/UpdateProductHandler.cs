using FluentValidation;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Domain.Exceptions;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.UpdateProduct;

public class UpdateProductHandler : IUpdateProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<UpdateProductRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IValidator<UpdateProductRequestDto> validator,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task HandleAsync(UpdateProductRequestDto request)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var product = await _productRepository.GetByIdAsync(request.Id)
            ?? throw new ProductNotFoundException(request.Id);

        if (await _productRepository.ExistsByNameAsync(request.Name, request.Id))
            throw new ProductNameAlreadyExistsException(request.Name);

        if (await _productRepository.ExistsByCodeAsync(request.Code, request.Id))
            throw new ProductCodeAlreadyExistsException(request.Code);

        if (!await _categoryRepository.ExistsAsync(request.CategoryId))
            throw new ProductCategoryNotFoundException(request.CategoryId);

        product.Update(
            request.Name,
            request.Description,
            request.Code,
            request.Care,
            request.Price,
            request.CategoryId);

        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync();
    }
}
