using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Focy.Restaurant.Menu
{
    public interface IMenuAppService : IApplicationService
    {
        Task<bool> CreateMenuItemAsync(MenuItemCreateDto input);

        Task<bool> UpdateMenuItemAsync(MenuItemUpdateDto input);

        Task<bool> DeleteMenuItemAsync(Guid id);

        Task<PagedResultDto<MenuItemDto>> GetMenuItemsAsync(MenuItemRequestDto input);
    }
}
