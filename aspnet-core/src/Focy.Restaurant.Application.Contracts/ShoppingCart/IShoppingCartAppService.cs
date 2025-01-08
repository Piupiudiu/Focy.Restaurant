using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Focy.Restaurant.ShoppingCart
{
    public interface IShoppingCartAppService : IApplicationService
    {
        Task<bool> CreateShoppingCartAsync(ShoppingCartCreateDto input);

        Task<bool> DeleteShoppingCartAsync(Guid id);

        Task<List<ShoppingCartItemDto>> GetAllShoppingCartItemsAsync();
    }
}
