using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Focy.Restaurant.Menu
{
    public class MenuItemRequestDto: PagedResultRequestDto
    {
        public string? Name { get; set; }
    }
}
