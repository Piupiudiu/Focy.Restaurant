using System;
using System.Collections.Generic;
using System.Text;
using Focy.Restaurant.Localization;
using Volo.Abp.Application.Services;

namespace Focy.Restaurant;

/* Inherit your application services from this class.
 */
public abstract class RestaurantAppService : ApplicationService
{
    protected RestaurantAppService()
    {
        LocalizationResource = typeof(RestaurantResource);
    }
}
