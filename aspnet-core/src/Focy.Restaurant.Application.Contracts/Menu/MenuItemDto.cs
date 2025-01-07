using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Focy.Restaurant.Menu
{
    public class MenuItemDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImgUri { get; set; }
        public string? Uri { get; set; }
        public bool IsAvailable { get; set; }
    }
}
