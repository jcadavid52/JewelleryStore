using FluentValidation.Results;
using JewelleryStore.Modules.Checkout.Exceptions;

namespace JewelleryStore.Modules.Checkout.UseCases;

internal static class ValidationResultExtensions
{
    public static CheckoutRequestValidationException ToRequestValidationException(this ValidationResult result)
    {
        var errors = result.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        return new CheckoutRequestValidationException("Uno o más campos no son válidos.", errors);
    }
}