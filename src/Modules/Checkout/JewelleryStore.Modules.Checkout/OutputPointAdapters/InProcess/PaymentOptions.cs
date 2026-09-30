namespace JewelleryStore.Modules.Checkout.OutputPointAdapters.InProcess;

public sealed class PaymentOptions
{
    public const string SectionName = "Payment";

    public bool SimulateFailure { get; set; }
}