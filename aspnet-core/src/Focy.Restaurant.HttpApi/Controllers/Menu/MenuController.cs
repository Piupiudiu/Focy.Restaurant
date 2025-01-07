using Focy.Restaurant.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Focy.Restaurant.Controllers.Menu
{
    [Route("api/menu")]
    public class MenuController(IMenuAppService menuAppService) : RestaurantController
    {
        [HttpPost("create")]
        public async Task<bool> CreateMenuItemAsync(MenuItemCreateDto input)
        {
            return await menuAppService.CreateMenuItemAsync(input);
        }
        [HttpPut("update")]
        public async Task<bool> UpdateMenuItemAsync(MenuItemUpdateDto input)
        {
            return await menuAppService.UpdateMenuItemAsync(input);
        }
        [HttpDelete("delete")]
        public async Task<bool> DeleteMenuItemAsync(Guid id)
        {
            return await menuAppService.DeleteMenuItemAsync(id);
        }
        [HttpGet("get")]
        public async Task<PagedResultDto<MenuItemDto>> GetMenuItemsAsync(string? name, int skipCount = 0, int maxResultCount = 10)
        {
            return await menuAppService.GetMenuItemsAsync(name, skipCount, maxResultCount);
        }
        [HttpPost("upload")]
        public async Task<string> UploadMenuImageAsync(Guid? id, IFormFile image)
        {
            using MemoryStream ms = new();
            await image.CopyToAsync(ms);
            return await menuAppService.UploadMenuImageAsync(id, ms.ToArray(), image.FileName);
        }
    }
}
