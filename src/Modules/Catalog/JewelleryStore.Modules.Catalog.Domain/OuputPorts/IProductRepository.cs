using JewelleryStore.Modules.Catalog.Domain.Entities;

namespace JewelleryStore.Modules.Catalog.Domain.OuputPorts
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
    }
}
