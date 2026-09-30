namespace JewelleryStore.Modules.Checkout.Exceptions;

public class CheckoutRequestValidationException : CheckoutException
{
    public override int StatusCode => 400;

    public CheckoutRequestValidationException(string message, IReadOnlyDictionary<string, string[]> errors)
        : base(message)
    {
        Errors = errors;
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }
}