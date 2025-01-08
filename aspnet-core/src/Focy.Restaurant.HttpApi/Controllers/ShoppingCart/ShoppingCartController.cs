using Focy.Restaurant.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Focy.Restaurant.Controllers.ShoppingCart
{
    [Route("api/shopping-cart")]
    public class ShoppingCartController(IShoppingCartAppService shoppingCartAppService) : RestaurantController
    {
        [HttpPut("create")]
        public async Task<bool> CreateShoppingCartAsync(ShoppingCartCreateDto input)
        {
            return await shoppingCartAppService.CreateShoppingCartAsync(input);
        }

        [HttpDelete("delete")]
        public async Task<bool> DeleteShoppingCartAsync(Guid id)
        {
            return await shoppingCartAppService.DeleteShoppingCartAsync(id);
        }

        [HttpGet("get-all")]
        public async Task<List<ShoppingCartItemDto>> GetAllShoppingCartItemsAsync()
        {
            return await shoppingCartAppService.GetAllShoppingCartItemsAsync();
        }
    }
}
