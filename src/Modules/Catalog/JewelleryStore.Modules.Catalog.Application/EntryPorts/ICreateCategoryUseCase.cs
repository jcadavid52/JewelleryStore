using JewelleryStore.Modules.Catalog.Application.UseCases.CreateCategory;

namespace JewelleryStore.Modules.Catalog.Application.EntryPorts;

public interface ICreateCategoryUseCase
{
    Task<CreateCategoryResponseDto> HandleAsync(CreateCategoryRequestDto request, CancellationToken cancellationToken = default);
}
