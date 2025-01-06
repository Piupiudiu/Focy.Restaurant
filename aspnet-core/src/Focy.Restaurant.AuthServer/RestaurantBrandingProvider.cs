using Microsoft.Extensions.Localization;
using Focy.Restaurant.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Focy.Restaurant;

[Dependency(ReplaceServices = true)]
public class RestaurantBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<RestaurantResource> _localizer;

    public RestaurantBrandingProvider(IStringLocalizer<RestaurantResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
