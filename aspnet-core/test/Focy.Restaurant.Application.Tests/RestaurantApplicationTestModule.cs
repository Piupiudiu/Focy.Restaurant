using Volo.Abp.Modularity;

namespace Focy.Restaurant;

[DependsOn(
    typeof(RestaurantApplicationModule),
    typeof(RestaurantDomainTestModule)
)]
public class RestaurantApplicationTestModule : AbpModule
{

}
