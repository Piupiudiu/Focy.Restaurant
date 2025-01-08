using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Focy.Restaurant.Order
{
    public class Order : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }

        public string? Remark { get; set; }

        public RestaurantEnum.OrderStatus Status { get; set; }

        protected Order() { }

        public Order(Guid id, string name, string? remark, RestaurantEnum.OrderStatus status)
            : base(id)
        {
            Id = id;
            Name = name;
            Remark = remark;
            Status = status;
        }
    }
}
