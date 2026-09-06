using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Domain.Entities;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.CreateCategory;

public class CreateCategoryHandler : ICreateCategoryUseCase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<CreateCategoryResponseDto> HandleAsync(CreateCategoryRequestDto request)
    {
        var category = new Category(request.Name, request.Description);

        _categoryRepository.Add(category);

        await _unitOfWork.SaveChangesAsync();

        return new CreateCategoryResponseDto(category.Id, category.Name, category.Description);
    }
}
