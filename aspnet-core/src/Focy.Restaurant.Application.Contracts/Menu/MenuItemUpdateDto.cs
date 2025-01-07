using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Focy.Restaurant.Menu
{
    public class MenuItemUpdateDto : EntityDto<Guid>
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? ImgUri { get; set; }

        public string? Uri { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
    }
}
