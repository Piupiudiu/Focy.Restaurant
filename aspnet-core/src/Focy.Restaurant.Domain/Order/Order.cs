using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Focy.Restaurant.Order
{
    public class Order : FullAuditedEntity<Guid>
    {
        public Guid MenuId { get; set; }

        public int Number { get; set; }


        protected Order() { }

        public Order(Guid id, Guid menuId, int number)
            : base(id)
        {
            Id = id;
            MenuId = menuId;
            Number = number;
        }
    }
}
