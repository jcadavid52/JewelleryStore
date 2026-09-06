using JewelleryStore.Modules.Catalog.Application.UseCases.CreateProduct;

namespace JewelleryStore.Modules.Catalog.Application.EntryPorts;

public interface ICreateProductUseCase
{
    Task<CreateProductResponseDto> HandleAsync(CreateProductRequestDto request);
}
