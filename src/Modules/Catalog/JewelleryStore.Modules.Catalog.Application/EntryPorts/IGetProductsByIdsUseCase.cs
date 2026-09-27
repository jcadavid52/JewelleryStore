using JewelleryStore.Modules.Catalog.Application.UseCases.GetProductsByIds;

namespace JewelleryStore.Modules.Catalog.Application.EntryPorts
{
    public interface IGetProductsByIdsUseCase
    {
        Task<GetProductsByIdsResponseDto> HandleAsync(
            GetProductsByIdsQueryDto query,
            CancellationToken cancellationToken = default);
    }
}