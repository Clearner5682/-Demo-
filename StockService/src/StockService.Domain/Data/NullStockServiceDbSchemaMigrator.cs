using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace StockService.Data;

/* This is used if database provider does't define
 * IStockServiceDbSchemaMigrator implementation.
 */
public class NullStockServiceDbSchemaMigrator : IStockServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
