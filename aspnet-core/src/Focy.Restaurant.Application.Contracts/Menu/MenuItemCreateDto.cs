﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focy.Restaurant.Menu
{
    public class MenuItemCreateDto
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? ImgUri { get; set; }

        public string? Uri { get; set; }
    }
}
