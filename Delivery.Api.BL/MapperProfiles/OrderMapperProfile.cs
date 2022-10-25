using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Extensions;
using Delivery.Common.Models.Order;

namespace Delivery.Api.BL.MapperProfiles
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<OrderEntity, OrderListModel>();
            CreateMap<OrderEntity, OrderDetailModel>();

            CreateMap<OrderDetailModel, OrderEntity>()
                .Ignore(dst => dst.Restaurant)
                .Ignore(dst => dst.DishAmounts);
        }
    }
}
