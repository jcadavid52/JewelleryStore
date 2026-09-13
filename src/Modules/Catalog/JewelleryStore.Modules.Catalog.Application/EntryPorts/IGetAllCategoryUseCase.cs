using JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCategory;

namespace JewelleryStore.Modules.Catalog.Application.EntryPorts;

public interface IGetAllCategoryUseCase
{
    Task<GetAllCategoryResponseDto> HandleAsync(GetAllCategoryQueryDto query, CancellationToken cancellationToken = default);
}