using OrderService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace OrderService;

[DependsOn(
    typeof(OrderServiceEntityFrameworkCoreTestModule)
    )]
public class OrderServiceDomainTestModule : AbpModule
{

}
