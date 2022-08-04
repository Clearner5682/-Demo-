using StockService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace StockService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class StockServiceController : AbpControllerBase
{
    protected StockServiceController()
    {
        LocalizationResource = typeof(StockServiceResource);
    }
}
