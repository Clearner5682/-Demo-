using Volo.Abp.Settings;

namespace OrderService.Settings;

public class OrderServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(OrderServiceSettings.MySetting1));
    }
}
