using Microsoft.AspNetCore.Mvc;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Application.UseCases.CreateProduct;
using JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCatalog;

namespace JewelleryStore.Modules.Catalog.Infrastructure.EntryPointAdapters.Rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ICreateProductUseCase _createProductUseCase;
    private readonly IGetAllCatalogUseCase _getAllCatalogUseCase;

    public ProductsController(
        ICreateProductUseCase createProductUseCase,
        IGetAllCatalogUseCase getAllCatalogUseCase)
    {
        _createProductUseCase = createProductUseCase;
        _getAllCatalogUseCase = getAllCatalogUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _createProductUseCase.HandleAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? searchTerm = null,
        [FromQuery] int? categoryId = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAllCatalogQueryDto(searchTerm, categoryId, pageNumber, pageSize);
        var result = await _getAllCatalogUseCase.HandleAsync(query, cancellationToken);
        return Ok(result);
    }
}
