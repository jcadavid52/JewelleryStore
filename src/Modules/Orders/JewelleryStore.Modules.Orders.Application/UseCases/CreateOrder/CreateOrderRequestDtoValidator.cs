using FluentValidation;

namespace JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

public class CreateOrderRequestDtoValidator : AbstractValidator<CreateOrderRequestDto>
{
    public CreateOrderRequestDtoValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("El id del cliente es obligatorio");

        RuleFor(x => x.ShippingAddress)
            .NotNull().WithMessage("La dirección de envío es obligatoria");

        RuleFor(x => x.ShippingAddress.Address)
            .NotEmpty().WithMessage("La dirección es obligatoria")
            .MaximumLength(200).WithMessage("La dirección no puede exceder los 200 caracteres");

        RuleFor(x => x.ShippingAddress.City)
            .NotEmpty().WithMessage("La ciudad es obligatoria")
            .MaximumLength(100).WithMessage("La ciudad no puede exceder los 100 caracteres");

        RuleFor(x => x.ShippingAddress.PostalCode)
            .NotEmpty().WithMessage("El código postal es obligatorio")
            .MaximumLength(20).WithMessage("El código postal no puede exceder los 20 caracteres");

        RuleFor(x => x.ShippingAddress.Phone)
            .NotEmpty().WithMessage("El teléfono es obligatorio")
            .MaximumLength(20).WithMessage("El teléfono no puede exceder los 20 caracteres");

        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("El pedido debe contener al menos un artículo");

        RuleForEach(x => x.Items)
            .SetValidator(new CreateOrderItemDtoValidator());
    }
}