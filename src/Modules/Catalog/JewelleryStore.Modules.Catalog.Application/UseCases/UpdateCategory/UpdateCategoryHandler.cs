using FluentValidation;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Domain.Exceptions;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.UpdateCategory;

public class UpdateCategoryHandler : IUpdateCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IValidator<UpdateCategoryRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryHandler(
        ICategoryRepository categoryRepository,
        IValidator<UpdateCategoryRequestDto> validator,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task HandleAsync(UpdateCategoryRequestDto request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new CategoryNotFoundException(request.Id);

        if (await _categoryRepository.ExistsByNameAsync(request.Name, request.Id, cancellationToken))
            throw new CategoryNameAlreadyExistsException(request.Name);

        category.Update(request.Name, request.Description);

        _categoryRepository.Update(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
