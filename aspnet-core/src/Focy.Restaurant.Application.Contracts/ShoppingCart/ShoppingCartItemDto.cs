using System;
using Volo.Abp.Application.Dtos;

namespace Focy.Restaurant.ShoppingCart
{
    public class ShoppingCartItemDto : EntityDto<Guid>
    {
        public Guid MenuId { get; set; }
        public string Name { get; set; }
        public string? ImgUri { get; set; }
    }
}
