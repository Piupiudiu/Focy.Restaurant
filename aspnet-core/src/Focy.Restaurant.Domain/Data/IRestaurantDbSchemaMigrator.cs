using System.Threading.Tasks;

namespace Focy.Restaurant.Data;

public interface IRestaurantDbSchemaMigrator
{
    Task MigrateAsync();
}
