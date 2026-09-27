using FluentValidation;

namespace JewelleryStore.Modules.Orders.Application.UseCases.CancelOrder;

public class CancelOrderRequestDtoValidator : AbstractValidator<CancelOrderRequestDto>
{
    public CancelOrderRequestDtoValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("El id del pedido es obligatorio");
    }
}