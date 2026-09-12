using FluentValidation;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Domain.Entities;
using JewelleryStore.Modules.Catalog.Domain.Exceptions;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.CreateCategory;

public class CreateCategoryHandler : ICreateCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<CreateCategoryRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandler(
        ICategoryRepository categoryRepository,
        IValidator<CreateCategoryRequestDto> validator,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<CreateCategoryResponseDto> HandleAsync(CreateCategoryRequestDto request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        if (await _categoryRepository.ExistsByNameAsync(request.Name, cancellationToken: cancellationToken))
            throw new CategoryNameAlreadyExistsException(request.Name);

        var category = new Category(request.Name, request.Description);

        _categoryRepository.Add(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateCategoryResponseDto(category.Id, category.Name, category.Description);
    }
}
