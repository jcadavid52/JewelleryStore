using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Domain.Exceptions;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.UpdateProduct;

public class UpdateProductHandler : IUpdateProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task HandleAsync(UpdateProductRequestDto request)
    {
        var product = await _productRepository.GetByIdAsync(request.Id)
            ?? throw new ProductNotFoundException(request.Id);

        if (await _productRepository.ExistsByNameAsync(request.Name, request.Id))
            throw new ProductNameAlreadyExistsException(request.Name);

        if (await _productRepository.ExistsByCodeAsync(request.Code, request.Id))
            throw new ProductCodeAlreadyExistsException(request.Code);

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
