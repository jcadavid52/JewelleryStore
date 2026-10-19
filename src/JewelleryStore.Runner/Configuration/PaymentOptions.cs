namespace JewelleryStore.Runner.Configuration
{
    public class PaymentOptions
    {
        public const string SectionName = "PaymentGateway";
        public string SecretKey { get; set; } = string.Empty;
    }
}
