using JewelleryStore.Modules.Orders.Domain.Entities;
using JewelleryStore.Modules.Orders.Domain.OuputPorts;

namespace JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.Database.SqlServer.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrdersDbContext _dbContext;

    public OrderRepository(OrdersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Order entity) => _dbContext.Orders.Add(entity);
}