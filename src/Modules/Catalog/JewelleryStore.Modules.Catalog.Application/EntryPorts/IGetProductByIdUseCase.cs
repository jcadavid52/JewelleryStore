using JewelleryStore.Modules.Catalog.Application.Dtos;

namespace JewelleryStore.Modules.Catalog.Application.EntryPorts;

public interface IGetProductByIdUseCase
{
    Task<ProductDto> HandleAsync(Guid id, CancellationToken cancellationToken = default);
}