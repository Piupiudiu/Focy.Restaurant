using System;
using System.ComponentModel.DataAnnotations;

namespace Focy.Restaurant.ShoppingCart
{
    public class ShoppingCartCreateDto
    {
        [Required]
        public Guid MenuId { get; set; }
    }
}
