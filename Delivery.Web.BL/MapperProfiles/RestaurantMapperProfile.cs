using AutoMapper;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Restaurant;


namespace Delivery.Web.BL.MapperProfiles
{
    class RestaurantMapperProfile : Profile
    {
        public RestaurantMapperProfile()
        {
            CreateMap<RestaurantDetailModel, RestaurantCreateModel>();
            CreateMap<RestaurantCreateModel, RestaurantDetailModel>();
        }
    }
}
