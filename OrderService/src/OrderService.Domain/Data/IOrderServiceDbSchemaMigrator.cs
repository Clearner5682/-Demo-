using System.Threading.Tasks;

namespace OrderService.Data;

public interface IOrderServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
