using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Focy.Restaurant.Order
{
    public class OrderItem : FullAuditedEntity<Guid>
    {
        public Guid OrderId { get; set; }

        public Guid MenuId {  get; set; }

        protected OrderItem() { }

        public OrderItem(Guid id, Guid orderId, Guid menuId)
            : base(id)
        {
            Id = id;
            OrderId = orderId;
            MenuId = menuId;
        }
    }
}
