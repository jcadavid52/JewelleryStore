using JewelleryStore.Modules.Catalog.Application.Dtos;

namespace JewelleryStore.Modules.Catalog.Application.EntryPorts;

public interface IGetCategoryByIdUseCase
{
    Task<CategoryDto> HandleAsync(int id, CancellationToken cancellationToken = default);
}