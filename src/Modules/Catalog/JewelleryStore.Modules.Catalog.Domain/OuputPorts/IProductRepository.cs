using JewelleryStore.Modules.Catalog.Domain.Entities;

namespace JewelleryStore.Modules.Catalog.Domain.OuputPorts
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<bool> ExistsByNameAsync(string name, Guid? exceptId = null);

        Task<bool> ExistsByCodeAsync(string code, Guid? exceptId = null);
    }
}
