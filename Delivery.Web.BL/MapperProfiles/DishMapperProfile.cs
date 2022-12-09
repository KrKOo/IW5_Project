using AutoMapper;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Restaurant;


namespace Delivery.Web.BL.MapperProfiles
{
    class DishMapperProfile : Profile
    {
        public DishMapperProfile()
        {
            CreateMap<DishDetailModel, DishCreateModel>();
            CreateMap<RestaurantDetailModel, RestaurantCreateModel>();
        }
    }
}
