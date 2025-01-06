using Volo.Abp.Modularity;

namespace Focy.Restaurant;

public abstract class RestaurantApplicationTestBase<TStartupModule> : RestaurantTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
