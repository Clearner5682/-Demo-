using StockService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace StockService;

[DependsOn(
    typeof(StockServiceEntityFrameworkCoreTestModule)
    )]
public class StockServiceDomainTestModule : AbpModule
{

}
