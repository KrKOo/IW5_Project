using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Extensions;
using Delivery.Common.Models.Restaurant;

namespace Delivery.Api.BL.MapperProfiles
{
    public class RestaurantMapperProfile : Profile
    {
        public RestaurantMapperProfile()
        {
            CreateMap<RestaurantEntity, RestaurantListModel>();
            CreateMap<RestaurantEntity, RestaurantDetailModel>();

            CreateMap<RestaurantCreateModel, RestaurantEntity>()
                .Ignore(dst => dst.Dishes)
                .Ignore(dst => dst.Orders);
        }
    }
}
