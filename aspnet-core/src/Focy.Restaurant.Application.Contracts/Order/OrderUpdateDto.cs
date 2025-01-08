using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Focy.Restaurant.Order
{
    public class OrderUpdateDto : EntityDto<Guid>
    {
        [Required]
        public RestaurantEnum.OrderStatus Status { get; set; }
    }
}
