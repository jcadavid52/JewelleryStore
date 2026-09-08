using FluentValidation;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.CreateProduct;

public class CreateProductRequestDtoValidator : AbstractValidator<CreateProductRequestDto>
{
    public CreateProductRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es obligatoria")
            .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("El código es obligatorio")
            .MaximumLength(50).WithMessage("El código no puede exceder los 50 caracteres");

        RuleFor(x => x.Care)
            .NotEmpty().WithMessage("Las indicaciones de cuidado son obligatorias")
            .MaximumLength(500).WithMessage("Las indicaciones de cuidado no pueden exceder los 500 caracteres");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor que 0");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("La categoría debe ser mayor que 0");
    }
}