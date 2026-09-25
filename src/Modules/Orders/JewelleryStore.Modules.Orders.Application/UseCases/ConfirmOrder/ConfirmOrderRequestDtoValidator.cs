using FluentValidation;

namespace JewelleryStore.Modules.Orders.Application.UseCases.ConfirmOrder;

public class ConfirmOrderRequestDtoValidator : AbstractValidator<ConfirmOrderRequestDto>
{
    public ConfirmOrderRequestDtoValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("El id del pedido es obligatorio");
    }
}