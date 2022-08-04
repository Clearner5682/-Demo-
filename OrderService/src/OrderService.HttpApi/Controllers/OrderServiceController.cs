using OrderService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace OrderService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class OrderServiceController : AbpControllerBase
{
    protected OrderServiceController()
    {
        LocalizationResource = typeof(OrderServiceResource);
    }
}
