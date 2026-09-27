using FluentValidation;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.GetProductsByIds
{
    public class GetProductsByIdsQueryDtoValidator : AbstractValidator<GetProductsByIdsQueryDto>
    {
        public GetProductsByIdsQueryDtoValidator()
        {
            RuleFor(x => x.ProductIds)
                .NotNull().WithMessage("La lista de productos es obligatoria")
                .NotEmpty().WithMessage("Debe indicar al menos un producto.");

            RuleForEach(x => x.ProductIds)
                .NotEmpty().WithMessage("El id de producto no puede ser vacío.");
        }
    }
}