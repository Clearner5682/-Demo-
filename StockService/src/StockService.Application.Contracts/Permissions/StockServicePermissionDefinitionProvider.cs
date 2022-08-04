using StockService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace StockService.Permissions;

public class StockServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(StockServicePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(StockServicePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<StockServiceResource>(name);
    }
}
