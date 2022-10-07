using System;
//using AutoMapper;

namespace Delivery.Api.DAL.Common.Entities
{
    public record RestaurantEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string GPS { get; set; }
        public double Revenue { get; set; }
        public string? LogoUrl { get; set; }
        public ICollection<DishEntity> Provides { get; set; } = new List<DishEntity>();
        public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();

        public RestaurantEntity(Guid id, string name, string description, string address, string gps, double revenue, string? logoUrl = null) : base(id)
        {
            Name = name;
            Description = description;
            Address = address;
            GPS = gps;
            Revenue = revenue;
            LogoUrl = logoUrl;
        }
    }

    //public class IngredientAmountEntityMapperProfile : Profile
    //{
    //    public IngredientAmountEntityMapperProfile()
    //    {
    //        CreateMap<IngredientAmountEntity, IngredientAmountEntity>();
    //    }
    //}
}
