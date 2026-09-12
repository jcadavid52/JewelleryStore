using Microsoft.AspNetCore.Mvc;
using JewelleryStore.Modules.Catalog.Application.EntryPorts;
using JewelleryStore.Modules.Catalog.Application.UseCases.CreateProduct;

namespace JewelleryStore.Modules.Catalog.Infrastructure.EntryPointAdapters.Rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ICreateProductUseCase _createProductUseCase;

    public ProductsController(ICreateProductUseCase createProductUseCase)
    {
        _createProductUseCase = createProductUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequestDto request, CancellationToken cancellationToken)
    {
        var result = await _createProductUseCase.HandleAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}
