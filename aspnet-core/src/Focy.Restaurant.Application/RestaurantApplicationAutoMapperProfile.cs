﻿using AutoMapper;
using Focy.Restaurant.Menu;

namespace Focy.Restaurant;

public class RestaurantApplicationAutoMapperProfile : Profile
{
    public RestaurantApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Menu.Menu, MenuItemDto>();
    }
}
