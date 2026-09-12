using JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCatalog;

namespace JewelleryStore.Modules.Catalog.Application.EntryPorts;

public interface IGetAllCatalogUseCase
{
    Task<GetAllCatalogResponseDto> HandleAsync(GetAllCatalogQueryDto query, CancellationToken cancellationToken = default);
}