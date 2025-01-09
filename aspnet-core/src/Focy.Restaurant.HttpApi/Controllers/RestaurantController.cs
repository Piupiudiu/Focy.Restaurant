using Focy.Restaurant.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Focy.Restaurant.Controllers;

/* Inherit your controllers from this class.
 */
[Authorize]
[ApiController]
[IgnoreAntiforgeryToken]
public abstract class RestaurantController : AbpControllerBase
{
    protected RestaurantController()
    {
        LocalizationResource = typeof(RestaurantResource);
    }
}
