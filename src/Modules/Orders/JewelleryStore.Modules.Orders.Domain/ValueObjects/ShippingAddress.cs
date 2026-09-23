namespace JewelleryStore.Modules.Orders.Domain.ValueObjects
{
    public sealed record ShippingAddress
    {
        public string Address { get; private set; } = string.Empty;
        public string City { get; private set; } = string.Empty;
        public string PostalCode { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;

        public ShippingAddress(
            string address,
            string city,
            string postalCode,
            string phone)
        {
            Address = address;
            City = city;
            PostalCode = postalCode;
            Phone = phone;
        }
    }
}
