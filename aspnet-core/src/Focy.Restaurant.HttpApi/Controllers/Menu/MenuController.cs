using Focy.Restaurant.Menu;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Focy.Restaurant.Controllers.Menu
{
    [Route("api/menu")]
    public class MenuController : RestaurantController
    {
        private readonly IMenuAppService _menuAppService;

        public MenuController(IMenuAppService menuAppService)
        {
            _menuAppService = menuAppService;
        }

        [HttpPost("create")]
        public async Task<bool> CreateMenuItemAsync(MenuItemCreateDto input)
        {
            return await _menuAppService.CreateMenuItemAsync(input);
        }
        [HttpPut("update")]
        public async Task<bool> UpdateMenuItemAsync(MenuItemUpdateDto input)
        {
            return await _menuAppService.UpdateMenuItemAsync(input);
        }
        [HttpDelete("delete")]
        public async Task<bool> DeleteMenuItemAsync(Guid id)
        {
            return await _menuAppService.DeleteMenuItemAsync(id);
        }
        [HttpPost("get")]
        public async Task<PagedResultDto<MenuItemDto>> GetMenuItemsAsync(MenuItemRequestDto input)
        {
            return await _menuAppService.GetMenuItemsAsync(input);
        }
    }
}
