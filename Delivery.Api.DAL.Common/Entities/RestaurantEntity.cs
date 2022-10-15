using System;
using Delivery.Common.Structures;
using AutoMapper;

namespace Delivery.Api.DAL.Common.Entities
{
    public record RestaurantEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public GPS Gps { get; set; }
        public string? LogoUrl { get; set; }
        public ICollection<DishEntity> Dishes { get; set; } = new List<DishEntity>();
        public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();

        public RestaurantEntity(Guid id, string name, string description, string address, GPS gps, string? logoUrl = null) : base(id)
        {
            Name = name;
            Description = description;
            Address = address;
            Gps = gps;
            LogoUrl = logoUrl;
        }
    }

    public class RestaurantEntityMapperProfile : Profile
    {
        public RestaurantEntityMapperProfile()
        {
            CreateMap<RestaurantEntity, RestaurantEntity>();
        }
    }
}
