using FluentValidation;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.ReleaseStockItem;

public class ReleaseStockItemRequestDtoValidator : AbstractValidator<ReleaseStockItemRequestDto>
{
    public ReleaseStockItemRequestDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("El id del producto es obligatorio");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor que 0");
    }
}