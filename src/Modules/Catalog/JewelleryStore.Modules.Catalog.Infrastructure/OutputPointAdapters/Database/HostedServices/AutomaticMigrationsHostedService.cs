using JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.HostedServices
{
    public class AutomaticMigrationsHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public AutomaticMigrationsHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var provider = _serviceProvider.CreateScope();
            var database = provider.ServiceProvider.GetRequiredService<CatalogDbContext>();
            if (database.Database.IsRelational())
            {
                try
                {
                    await database.Database.MigrateAsync(cancellationToken);
                }
                catch (SqlException ex) when (ex.Number == 1801)
                {
                    await database.Database.MigrateAsync(cancellationToken);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
