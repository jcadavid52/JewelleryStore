using FluentValidation;

namespace JewelleryStore.Modules.Checkout.UseCases.Checkout;

public class CheckoutItemDtoValidator : AbstractValidator<CheckoutItemDto>
{
    public CheckoutItemDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("El id del producto es obligatorio");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("La cantidad debe ser mayor que 0");
    }
}