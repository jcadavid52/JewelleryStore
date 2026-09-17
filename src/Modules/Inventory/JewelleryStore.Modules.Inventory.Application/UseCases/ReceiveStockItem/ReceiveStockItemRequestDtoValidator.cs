using FluentValidation;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.ReceiveStockItem;

public class ReceiveStockItemRequestDtoValidator : AbstractValidator<ReceiveStockItemRequestDto>
{
    public ReceiveStockItemRequestDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("El id del producto es obligatorio");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor que 0");
    }
}
