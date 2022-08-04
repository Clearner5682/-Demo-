using Volo.Abp.Settings;

namespace StockService.Settings;

public class StockServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(StockServiceSettings.MySetting1));
    }
}
