using Focy.Restaurant.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Focy.Restaurant.Order
{
    public class OrderAppService(
        IRepository<Order, Guid> orderRepository,
        IRepository<OrderItem, Guid> orderItemRepository,
        IRepository<Menu.Menu, Guid> menuRepository,
        IRepository<ShoppingCart.ShoppingCart, Guid> shoppingCartRepository) : RestaurantAppService, IOrderAppService
    {
        public async Task<bool> CreateOrderAsync(OrderCreateDto input)
        {
            Order order = new(GuidGenerator.Create(), input.Name, input.Remark, RestaurantEnum.OrderStatus.Pending);
            List<OrderItem> orderItems = [];
            foreach (Guid menuId in input.MenuIds)
            {
                OrderItem orderItem = new(GuidGenerator.Create(), order.Id, menuId);
                orderItems.Add(orderItem);
            }
            await orderRepository.InsertAsync(order);
            await orderItemRepository.InsertManyAsync(orderItems);
            await shoppingCartRepository.DeleteDirectAsync(x => x.CreatorId == CurrentUser.Id && input.MenuIds.Contains(x.MenuId));
            return true;
        }

        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            await orderRepository.DeleteDirectAsync(x => x.Id == id);
            return true;
        }

        public async Task<bool> UpdateOrderAsync(OrderUpdateDto input)
        {
            Order order = await orderRepository.GetAsync(x => x.Id == input.Id);
            order.Status = input.Status;
            await orderRepository.UpdateAsync(order);
            return true;
        }

        public async Task<PagedResultDto<OrderDetailDto>> GetOrdersAsync(int skipCount = 0, int maxResultCount = 10, RestaurantEnum.OrderStatus? status = null)
        {
            List<Order> orders = [.. (await orderRepository.GetQueryableAsync())
                .Where(x => x.CreatorId == CurrentUser.Id)
                .WhereIf(status.HasValue, x => x.Status == status)
                .OrderByDescending(x => x.CreationTime)
                .Skip(skipCount)
                .Take(maxResultCount)];
            var orderDetails = (from o in await orderRepository.GetQueryableAsync()
                        where orders.Select(x => x.Id).Contains(o.Id)
                        join oi in await orderItemRepository.GetQueryableAsync() on o.Id equals oi.OrderId
                        join m in await menuRepository.GetQueryableAsync() on oi.MenuId equals m.Id
                        select new
                        {
                            o.Id,
                            o.Name,
                            o.Remark,
                            o.Status,
                            o.CreationTime,
                            MenuId = m.Id,
                            MenuName = m.Name,
                            m.Description,
                            m.ImgUri,
                            m.Uri,
                            m.IsAvailable
                        }).GroupBy(x => x.Id).OrderByDescending(x => x.Key);
            return new()
            {
                Items = [.. orderDetails.Select(x => new OrderDetailDto
                {
                    Id = x.Key,
                    Name = x.First().Name,
                    Remark = x.First().Remark,
                    Status = x.First().Status,
                    Menus = x.Select(y => new MenuItemDto
                    {
                        Id = y.MenuId,
                        Name = y.MenuName,
                        Description = y.Description,
                        ImgUri = y.ImgUri,
                        Uri = y.Uri,
                        IsAvailable = y.IsAvailable
                    }).ToList()
                })],
                TotalCount = (await orderRepository.GetQueryableAsync()).Count(x => x.CreatorId == CurrentUser.Id)
            };
        }
    }
}
