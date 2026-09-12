using JewelleryStore.Modules.Catalog.Domain.Entities;

namespace JewelleryStore.Modules.Catalog.Domain.OuputPorts
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<bool> ExistsByNameAsync(string name, Guid? exceptId = null, CancellationToken cancellationToken = default);

        Task<bool> ExistsByCodeAsync(string code, Guid? exceptId = null, CancellationToken cancellationToken = default);
    }
}
