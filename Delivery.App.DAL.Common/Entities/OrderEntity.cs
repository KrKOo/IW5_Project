using System;
using Delivery.Common.Enums;

//using AutoMapper;

namespace Delivery.Api.DAL.Common.Entities
{
    public record OrderEntity : EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public string Note { get; set; }
        public OrderStates State { get; set; }
        public UserEntity CreatedBy { get; set; }

        public ICollection<DishEntity> Dishes { get; set; } = new List<DishEntity>();

        public OrderEntity(Guid id, string name, string address, TimeSpan deliveryTime, string note, OrderStates state, UserEntity createdBy) : base(id)
        {
            Name = name;
            Address = address;
            DeliveryTime = deliveryTime;
            Note = note;
            State = state;
            CreatedBy = createdBy;
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
