using Volo.Abp.Modularity;

namespace Focy.Restaurant;

[DependsOn(
    typeof(RestaurantDomainModule),
    typeof(RestaurantTestBaseModule)
)]
public class RestaurantDomainTestModule : AbpModule
{

}
