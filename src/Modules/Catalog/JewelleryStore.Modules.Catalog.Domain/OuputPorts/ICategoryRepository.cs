using JewelleryStore.Modules.Catalog.Domain.Entities;

namespace JewelleryStore.Modules.Catalog.Domain.OuputPorts
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<(IEnumerable<Category> Categories, int TotalCount)> GetAllWithFiltersAsync(
            string? searchTerm = null,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name, int? exceptId = null, CancellationToken cancellationToken = default);
    }
}
