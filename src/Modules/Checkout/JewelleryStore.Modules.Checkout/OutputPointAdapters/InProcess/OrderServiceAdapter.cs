using JewelleryStore.Modules.Checkout.Services;
using JewelleryStore.Modules.Checkout.UseCases.CreatePaymentIntent;
using JewelleryStore.Modules.Orders.Contracts;

namespace JewelleryStore.Modules.Checkout.OutputPointAdapters.InProcess
{
    public class OrderServiceAdapter : IOrderService
    {
        private readonly ICreateOrderService _createOrderService;

        public OrderServiceAdapter(ICreateOrderService createOrderService)
        {
            _createOrderService = createOrderService;
        }

        public async Task<CreateOrderResponseService> CreateAsync(CreatePaymentIntentRequestDto request, CancellationToken cancellationToken = default)
        {
            var orderShippingAddress = new CreateOrderShippingAddressRequest(
                "Vereda Hato Viejo", "Bello", "0069", "3127118937");

            var items = request.Items.Select(item => new CreateOrderRequestItem(item.ProductId, item.Quantity));

            var result = await _createOrderService.CreateAsync(
                new CreateOrderRequest(request.CustomerId, orderShippingAddress, items.ToList()),
                cancellationToken);

            return new CreateOrderResponseService(result.Id, result.Total);
        }
    }
}
