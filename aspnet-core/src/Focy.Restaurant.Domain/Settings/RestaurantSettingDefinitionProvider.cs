using Volo.Abp.Settings;

namespace Focy.Restaurant.Settings;

public class RestaurantSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(RestaurantSettings.MySetting1));
    }
}
