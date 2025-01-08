using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Focy.Restaurant.Order
{
    public class OrderCreateDto
    {
        [Required]
        public string Name { get; set; }

        public string? Remark { get; set; }

        [Required]
        [MinLength(1)]
        public List<Guid> MenuIds { get; set; } = [];
    }
}
