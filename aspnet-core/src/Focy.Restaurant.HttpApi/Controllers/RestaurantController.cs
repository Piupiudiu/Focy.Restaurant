using Focy.Restaurant.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Focy.Restaurant.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class RestaurantController : AbpControllerBase
{
    protected RestaurantController()
    {
        LocalizationResource = typeof(RestaurantResource);
    }
}
