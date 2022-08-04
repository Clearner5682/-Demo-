using StockService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace StockService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(StockServiceEntityFrameworkCoreModule),
    typeof(StockServiceApplicationContractsModule)
    )]
public class StockServiceDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
