using Focy.Restaurant.Samples;
using Xunit;

namespace Focy.Restaurant.EntityFrameworkCore.Applications;

[Collection(RestaurantTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<RestaurantEntityFrameworkCoreTestModule>
{

}
