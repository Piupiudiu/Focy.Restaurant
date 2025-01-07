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
    public class MenuAppService : RestaurantAppService, IMenuAppService
    {
        private readonly IRepository<Menu, Guid> _menuRepository;

        public MenuAppService(IRepository<Menu, Guid> menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<bool> CreateMenuItemAsync(MenuItemCreateDto input)
        {
            Menu menu = new(GuidGenerator.Create(), input.Name, input.Description, input.Uri, true);
            await _menuRepository.InsertAsync(menu);
            return true;
        }

        public async Task<bool> DeleteMenuItemAsync(Guid id)
        {
            await _menuRepository.DeleteDirectAsync(x => x.Id == id);
            return true;
        }

        [DisableEntityChangeTracking]
        public async Task<PagedResultDto<MenuItemDto>> GetMenuItemsAsync(MenuItemRequestDto input)
        {
            List<Menu> res = [.. (await _menuRepository.GetQueryableAsync())
                .WhereIf(!input.Name.IsNullOrEmpty(), x => x.Name.Contains(input.Name!))
                .Take(input.MaxResultCount)
                .Skip(input.SkipCount)
                .OrderByDescending(x => x.CreationTime)];
            return new()
            {
                Items = ObjectMapper.Map<List<Menu>, List<MenuItemDto>>(res),
                TotalCount = await _menuRepository.GetCountAsync()
            };
        }

        public async Task<bool> UpdateMenuItemAsync(MenuItemUpdateDto input)
        {
            Menu menu = await _menuRepository.GetAsync(x => x.Id == input.Id);
            menu.Name = input.Name;
            menu.Description = input.Description;
            menu.Uri = input.Uri;
            menu.IsAvailable = input.IsAvailable;
            await _menuRepository.UpdateAsync(menu);
            return true;
        }
    }
}
