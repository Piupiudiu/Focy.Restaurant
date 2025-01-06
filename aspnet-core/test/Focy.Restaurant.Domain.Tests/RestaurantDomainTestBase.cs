using Volo.Abp.Modularity;

namespace Focy.Restaurant;

/* Inherit from this class for your domain layer tests. */
public abstract class RestaurantDomainTestBase<TStartupModule> : RestaurantTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
