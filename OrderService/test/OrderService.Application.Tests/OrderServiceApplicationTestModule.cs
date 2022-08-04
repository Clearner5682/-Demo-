using Volo.Abp.Modularity;

namespace OrderService;

[DependsOn(
    typeof(OrderServiceApplicationModule),
    typeof(OrderServiceDomainTestModule)
    )]
public class OrderServiceApplicationTestModule : AbpModule
{

}
