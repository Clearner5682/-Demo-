using System;
using System.Collections.Generic;
using System.Text;
using OrderService.Localization;
using Volo.Abp.Application.Services;

namespace OrderService;

/* Inherit your application services from this class.
 */
public abstract class OrderServiceAppService : ApplicationService
{
    protected OrderServiceAppService()
    {
        LocalizationResource = typeof(OrderServiceResource);
    }
}
