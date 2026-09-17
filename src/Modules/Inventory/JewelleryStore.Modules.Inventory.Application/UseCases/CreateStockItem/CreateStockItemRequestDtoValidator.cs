using FluentValidation;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.CreateStockItem;

public class CreateStockItemRequestDtoValidator : AbstractValidator<CreateStockItemRequestDto>
{
    public CreateStockItemRequestDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("El id del producto es obligatorio");
    }
}
