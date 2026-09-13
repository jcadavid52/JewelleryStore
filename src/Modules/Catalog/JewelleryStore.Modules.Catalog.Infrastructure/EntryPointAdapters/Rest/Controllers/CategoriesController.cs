using Microsoft.AspNetCore.Mvc;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Application.UseCases.CreateCategory;
using JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCategory;

namespace JewelleryStore.Modules.Catalog.Infrastructure.EntryPointAdapters.Rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICreateCategoryUseCase _createCategoryUseCase;
    private readonly IGetAllCategoryUseCase _getAllCategoryUseCase;
    private readonly IGetCategoryByIdUseCase _getCategoryByIdUseCase;

    public CategoriesController(
        ICreateCategoryUseCase createCategoryUseCase,
        IGetAllCategoryUseCase getAllCategoryUseCase,
        IGetCategoryByIdUseCase getCategoryByIdUseCase)
    {
        _createCategoryUseCase = createCategoryUseCase;
        _getAllCategoryUseCase = getAllCategoryUseCase;
        _getCategoryByIdUseCase = getCategoryByIdUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _createCategoryUseCase.HandleAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? searchTerm = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllCategoryQueryDto(searchTerm, pageNumber, pageSize);
        var result = await _getAllCategoryUseCase.HandleAsync(query, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _getCategoryByIdUseCase.HandleAsync(id, cancellationToken);
        return Ok(result);
    }
}
