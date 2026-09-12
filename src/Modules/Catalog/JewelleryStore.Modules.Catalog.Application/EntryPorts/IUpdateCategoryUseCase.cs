using JewelleryStore.Modules.Catalog.Application.UseCases.UpdateCategory;

namespace JewelleryStore.Modules.Catalog.Application.EntryPorts;

public interface IUpdateCategoryUseCase
{
    Task HandleAsync(UpdateCategoryRequestDto request, CancellationToken cancellationToken = default);
}
