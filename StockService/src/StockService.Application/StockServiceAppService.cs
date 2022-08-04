using System;
using System.Collections.Generic;
using System.Text;
using StockService.Localization;
using Volo.Abp.Application.Services;

namespace StockService;

/* Inherit your application services from this class.
 */
public abstract class StockServiceAppService : ApplicationService
{
    protected StockServiceAppService()
    {
        LocalizationResource = typeof(StockServiceResource);
    }
}
