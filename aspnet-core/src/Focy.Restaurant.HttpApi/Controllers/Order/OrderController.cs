using Focy.Restaurant.Order;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Focy.Restaurant.Controllers.Order
{
    [Route("api/order")]
    public class OrderController(IOrderAppService orderAppService) : RestaurantController
    {
        [HttpPost("create")]
        public async Task<bool> CreateOrderAsync(OrderCreateDto input)
        {
            return await orderAppService.CreateOrderAsync(input);
        }

        [HttpDelete("delete")]
        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            return await orderAppService.DeleteOrderAsync(id);
        }

        [HttpPut("update")]
        public async Task<bool> UpdateOrderAsync(OrderUpdateDto input)
        {
            return await orderAppService.UpdateOrderAsync(input);
        }

        [HttpGet("get")]
        public async Task<PagedResultDto<OrderDetailDto>> GetOrdersAsync(int skipCount = 0, int maxResultCount = 10)
        {
            return await orderAppService.GetOrdersAsync(skipCount, maxResultCount);
        }
    }
}
