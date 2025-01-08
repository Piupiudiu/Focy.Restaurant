using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Focy.Restaurant.Order
{
    public interface IOrderAppService : IApplicationService
    {
        Task<bool> CreateOrderAsync(OrderCreateDto input);

        Task<bool> DeleteOrderAsync(Guid id);

        Task<bool> UpdateOrderAsync(OrderUpdateDto input);

        Task<PagedResultDto<OrderDetailDto>> GetOrdersAsync(int skipCount = 0, int maxResultCount = 10);
    }
}
