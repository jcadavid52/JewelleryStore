using JewelleryStore.Modules.Orders.Domain.Entities;

namespace JewelleryStore.Modules.Orders.Domain.OuputPorts
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
