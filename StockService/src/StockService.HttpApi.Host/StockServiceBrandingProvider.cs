using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace StockService;

[Dependency(ReplaceServices = true)]
public class StockServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "StockService";
}
