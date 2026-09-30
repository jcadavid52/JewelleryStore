using FluentValidation;

namespace JewelleryStore.Modules.Checkout.UseCases.Checkout;

public class CheckoutRequestDtoValidator : AbstractValidator<CheckoutRequestDto>
{
    public CheckoutRequestDtoValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("El id del cliente es obligatorio");

        RuleFor(x => x.ShippingAddress)
            .NotNull().WithMessage("La direcci�n de env�o es obligatoria");

        RuleFor(x => x.ShippingAddress.Address)
            .NotEmpty().WithMessage("La direcci�n es obligatoria")
            .MaximumLength(200).WithMessage("La direcci�n no puede exceder los 200 caracteres");

        RuleFor(x => x.ShippingAddress.City)
            .NotEmpty().WithMessage("La ciudad es obligatoria")
            .MaximumLength(100).WithMessage("La ciudad no puede exceder los 100 caracteres");

        RuleFor(x => x.ShippingAddress.PostalCode)
            .NotEmpty().WithMessage("El c�digo postal es obligatorio")
            .MaximumLength(20).WithMessage("El c�digo postal no puede exceder los 20 caracteres");

        RuleFor(x => x.ShippingAddress.Phone)
            .NotEmpty().WithMessage("El tel�fono es obligatorio")
            .MaximumLength(20).WithMessage("El tel�fono no puede exceder los 20 caracteres");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("El pedido debe contener al menos un art�culo")
            .NotEmpty().WithMessage("El pedido debe contener al menos un art�culo");

        RuleForEach(x => x.Items)
            .SetValidator(new CheckoutItemDtoValidator());
    }
}