using JewelleryStore.Modules.Catalog.Domain.Entities;

namespace JewelleryStore.Modules.Catalog.Domain.OuputPorts
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<(IEnumerable<Product> Products, int TotalCount)> GetAllWithFiltersAsync(
            string? searchTerm = null,
            int? categoryId = null,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);

        Task<bool> ExistsByNameAsync(string name, Guid? exceptId = null, CancellationToken cancellationToken = default);

        Task<bool> ExistsByCodeAsync(string code, Guid? exceptId = null, CancellationToken cancellationToken = default);
    }
}
