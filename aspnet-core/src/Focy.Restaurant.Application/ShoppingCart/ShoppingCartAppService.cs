using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Focy.Restaurant.ShoppingCart
{
    public class ShoppingCartAppService(
        IRepository<ShoppingCart, Guid> shoppingCartRepository,
        IRepository<Menu.Menu, Guid> menuRepository) : RestaurantAppService, IShoppingCartAppService
    {
        public async Task<bool> CreateShoppingCartAsync(ShoppingCartCreateDto input)
        {
            if (await shoppingCartRepository.AnyAsync(x => x.MenuId == input.MenuId && x.CreatorId == CurrentUser.Id))
            {
                return true;
            }
            else
            {
                await shoppingCartRepository.InsertAsync(new ShoppingCart(GuidGenerator.Create(), input.MenuId));
                return true;
            }
        }

        public async Task<bool> DeleteShoppingCartAsync(Guid id)
        {
            await shoppingCartRepository.DeleteDirectAsync(x => x.Id == id);
            return true;
        }

        public async Task<List<ShoppingCartItemDto>> GetAllShoppingCartItemsAsync()
        {
            return [.. (from sc in await shoppingCartRepository.GetQueryableAsync()
                    join m in await menuRepository.GetQueryableAsync() on sc.MenuId equals m.Id
                    where sc.CreatorId == CurrentUser.Id
                    orderby sc.CreationTime descending
                    select new ShoppingCartItemDto()
                    {
                        Id = sc.Id,
                        MenuId = sc.MenuId,
                        ImgUri = m.ImgUri,
                        Name = m.Name
                    })];
        }
    }
}
