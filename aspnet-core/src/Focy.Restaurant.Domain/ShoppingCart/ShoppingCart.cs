using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Focy.Restaurant.ShoppingCart
{
    public class ShoppingCart : FullAuditedEntity<Guid>
    {
        public Guid MenuId { get; set; }

        protected ShoppingCart() { }

        public ShoppingCart(Guid id, Guid menuId)
            : base(id)
        {
            Id = id;
            MenuId = menuId;
        }
    }
}
