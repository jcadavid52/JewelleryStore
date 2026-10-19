namespace JewelleryStore.Modules.Checkout.Exceptions
{
    public class CreatePaymentIntentException : CheckoutException
    {
        public override int StatusCode => 500;
        public CreatePaymentIntentException(string message)
            : base(message)
        {
        }
    }
}
