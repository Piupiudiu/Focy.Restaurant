using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.ChangeTracking;
using Volo.Abp.Domain.Repositories;

namespace Focy.Restaurant.Menu
{
    public class MenuAppService(
        IRepository<Menu, Guid> menuRepository,
        IBlobContainerFactory blobContainerFactory) : RestaurantAppService, IMenuAppService
    {
        public async Task<bool> CreateMenuItemAsync(MenuItemCreateDto input)
        {
            Menu menu = new(GuidGenerator.Create(), input.Name, input.Description, input.ImgUri, input.Uri, true);
            await menuRepository.InsertAsync(menu);
            return true;
        }

        public async Task<bool> DeleteMenuItemAsync(Guid id)
        {
            var menu = await menuRepository.GetAsync(id);
            if (!menu.ImgUri.IsNullOrEmpty())
            {
                var imageBlob = blobContainerFactory.Create("menuImages");
                await imageBlob.DeleteAsync(menu.ImgUri.Split("/")[^1]);
            }
            await menuRepository.DeleteDirectAsync(x => x.Id == id);
            return true;
        }

        [DisableEntityChangeTracking]
        public async Task<PagedResultDto<MenuItemDto>> GetMenuItemsAsync(string? name, int skipCount = 0, int maxResultCount = 10)
        {
            List<Menu> res = [.. (await menuRepository.GetQueryableAsync())
                .WhereIf(!name.IsNullOrEmpty(), x => x.Name.Contains(name!))
                .Skip(skipCount)
                .Take(maxResultCount)
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
            menu.ImgUri = input.ImgUri;
            menu.Uri = input.Uri;
            menu.IsAvailable = input.IsAvailable;
            await menuRepository.UpdateAsync(menu);
            return true;
        }

        public async Task<string> UploadMenuImageAsync(Guid? id, byte[] image, string filename)
        {
            var imageBlob = blobContainerFactory.Create("menuImages");
            if (id.HasValue)
            {
                Menu menu = await menuRepository.GetAsync(x => x.Id == id);
                if (!menu.ImgUri.IsNullOrEmpty())
                {
                    await imageBlob.DeleteAsync(menu.ImgUri.Split("/")[^1]);
                }
            }
            var targetFileName = $@"{GuidGenerator.Create()}.{filename.Split(".")[^1]}";
            await imageBlob.SaveAsync(targetFileName, image);
            return $@"{CurrentTenant?.Name ?? "host"}/menuImages/{targetFileName}";
        }
    }
}
