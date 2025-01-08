using Focy.Restaurant.Menu;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Focy.Restaurant.Order
{
    public class OrderDetailDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string? Remark { get; set; }
        public RestaurantEnum.OrderStatus Status { get; set; }
        public List<MenuItemDto> Menus { get; set; } = [];
    }
}
