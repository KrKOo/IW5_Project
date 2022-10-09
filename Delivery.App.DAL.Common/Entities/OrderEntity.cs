using System;
using Delivery.Common.Enums;
//using AutoMapper;

namespace Delivery.Api.DAL.Common.Entities
{
    public record OrderEntity : EntityBase
    {
        public string Address { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public string Note { get; set; }
        public OrderStates State { get; set; }
        public Guid RestaurantId { get; set; }
        public RestaurantEntity? Restaurant { get; set; }

        public ICollection<DishAmountEntity> DishAmounts { get; set; } = new List<DishAmountEntity>();

        public OrderEntity(Guid id, string address, TimeSpan deliveryTime, string note, OrderStates state, Guid restaurantId) : base(id)
        {
            Address = address;
            DeliveryTime = deliveryTime;
            Note = note;
            State = state;
            RestaurantId = restaurantId;
        }
    }

    //public class IngredientEntityMapperProfile : Profile
    //{
    //    public IngredientEntityMapperProfile()
    //    {
    //        CreateMap<IngredientEntity, IngredientEntity>();
    //    }
    //}
}
