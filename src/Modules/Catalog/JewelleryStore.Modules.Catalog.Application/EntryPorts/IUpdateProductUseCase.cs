using JewelleryStore.Modules.Catalog.Application.UseCases.UpdateProduct;

namespace JewelleryStore.Modules.Catalog.Application.EntryPorts;

public interface IUpdateProductUseCase
{
    Task HandleAsync(UpdateProductRequestDto request, CancellationToken cancellationToken = default);
}
