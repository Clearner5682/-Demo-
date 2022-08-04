using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OrderService.Data;

/* This is used if database provider does't define
 * IOrderServiceDbSchemaMigrator implementation.
 */
public class NullOrderServiceDbSchemaMigrator : IOrderServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
