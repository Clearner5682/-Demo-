using Volo.Abp.Modularity;

namespace StockService;

[DependsOn(
    typeof(StockServiceApplicationModule),
    typeof(StockServiceDomainTestModule)
    )]
public class StockServiceApplicationTestModule : AbpModule
{

}
