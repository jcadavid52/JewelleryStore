namespace JewelleryStore.Modules.Checkout.Exceptions;

public abstract class CheckoutException : Exception
{
    public abstract int StatusCode { get; }

    protected CheckoutException(string message)
        : base(message)
    {
    }
}