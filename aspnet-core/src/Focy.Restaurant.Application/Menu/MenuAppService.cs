using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.ChangeTracking;
using Volo.Abp.Domain.Repositories;

namespace Focy.Restaurant.Menu
{
    public class MenuAppService(IRepository<Menu> menuRepository) : RestaurantAppService, IMenuAppService
    {
        public async Task<bool> CreateMenuItemAsync(MenuItemCreateDto input)
        {
            Menu menu = new(GuidGenerator.Create(), input.Name, input.Description, input.Uri, true);
            await menuRepository.InsertAsync(menu);
            return true;
        }

        public async Task<bool> DeleteMenuItemAsync(Guid id)
        {
            await menuRepository.DeleteDirectAsync(x => x.Id == id);
            return true;
        }

        [DisableEntityChangeTracking]
        public async Task<PagedResultDto<MenuItemDto>> GetMenuItemsAsync(MenuItemRequestDto input)
        {
            List<Menu> res = [.. (await menuRepository.GetQueryableAsync())
                .WhereIf(!input.Name.IsNullOrEmpty(), x => x.Name.Contains(input.Name!))
                .Take(input.MaxResultCount)
                .Skip(input.SkipCount)
                .OrderByDescending(x => x.CreationTime)];
            return new()
            {
                Items = ObjectMapper.Map<List<Menu>, List<MenuItemDto>>(res),
                TotalCount = await menuRepository.GetCountAsync()
            };
        }

        public async Task<bool> UpdateMenuItemAsync(MenuItemUpdateDto input)
        {
            Menu menu = await menuRepository.GetAsync(x => x.Id == input.Id);
            menu.Name = input.Name;
            menu.Description = input.Description;
            menu.Uri = input.Uri;
            menu.IsAvailable = input.IsAvailable;
            await menuRepository.UpdateAsync(menu);
            return true;
        }
    }
}
