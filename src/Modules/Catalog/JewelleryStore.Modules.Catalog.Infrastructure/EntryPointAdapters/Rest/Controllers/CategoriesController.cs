using Microsoft.AspNetCore.Mvc;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Application.UseCases.CreateCategory;

namespace JewelleryStore.Modules.Catalog.Infrastructure.EntryPointAdapters.Rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICreateCategoryUseCase _createCategoryUseCase;

    public CategoriesController(ICreateCategoryUseCase createCategoryUseCase)
    {
        _createCategoryUseCase = createCategoryUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto request)
    {
        var result = await _createCategoryUseCase.HandleAsync(request);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}
