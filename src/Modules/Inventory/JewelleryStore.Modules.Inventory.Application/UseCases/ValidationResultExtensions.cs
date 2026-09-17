using FluentValidation.Results;
using JewelleryStore.Modules.Inventory.Domain.Exceptions;

namespace JewelleryStore.Modules.Inventory.Application.UseCases;

internal static class ValidationResultExtensions
{
    public static RequestValidationException ToRequestValidationException(this ValidationResult result)
    {
        var errors = result.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        return new RequestValidationException("Uno o más campos no son válidos.", errors);
    }
}
