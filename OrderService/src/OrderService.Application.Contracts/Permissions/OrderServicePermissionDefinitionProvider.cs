using OrderService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace OrderService.Permissions;

public class OrderServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(OrderServicePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(OrderServicePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<OrderServiceResource>(name);
    }
}
