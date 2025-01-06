using Xunit;

namespace Focy.Restaurant.EntityFrameworkCore;

[CollectionDefinition(RestaurantTestConsts.CollectionDefinitionName)]
public class RestaurantEntityFrameworkCoreCollection : ICollectionFixture<RestaurantEntityFrameworkCoreFixture>
{

}
