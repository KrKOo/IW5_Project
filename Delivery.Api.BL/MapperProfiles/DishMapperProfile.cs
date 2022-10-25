using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Extensions;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.OrderDish;


namespace Delivery.Api.BL.MapperProfiles
{
    public class DishMapperProfile : Profile
    {
        public DishMapperProfile()
        {
            CreateMap<DishEntity, DishListModel>();
            CreateMap<DishEntity, DishDetailModel>();

            CreateMap<DishCreateModel, DishEntity>()
                .Ignore(dst => dst.Restaurant)
                .Ignore(dst => dst.DishAmounts);

            CreateMap<DishAmountEntity, OrderDishDetailModel>();
        }
    }
}
