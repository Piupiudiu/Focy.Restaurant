using Focy.Restaurant.Samples;
using Xunit;

namespace Focy.Restaurant.EntityFrameworkCore.Domains;

[Collection(RestaurantTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<RestaurantEntityFrameworkCoreTestModule>
{

}
