using System.Threading.Tasks;

namespace StockService.Data;

public interface IStockServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
