using JewelleryStore.Modules.Catalog.Domain.Entities;

namespace JewelleryStore.Modules.Catalog.Domain.OuputPorts
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name, int? exceptId = null, CancellationToken cancellationToken = default);
    }
}
