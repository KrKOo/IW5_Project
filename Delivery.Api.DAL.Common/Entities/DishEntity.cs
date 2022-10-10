using System;
//using AutoMapper;

namespace Delivery.Api.DAL.Common.Entities
{
    public record DishEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Allergens { get; set; } //TODO: What about enum??
        public string? ImageUrl { get; set; }
        public ICollection<RestaurantEntity> Restaurants { get; set; } = new List<RestaurantEntity>();

        public DishEntity(Guid id, string name, string description, double price, string allergens, string? imageUrl = null) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            Allergens = allergens;
            ImageUrl = imageUrl;
        }
    }

    //public class RecipeEntityMapperProfile : Profile
    //{
    //    public RecipeEntityMapperProfile()
    //    {
    //        CreateMap<RecipeEntity, RecipeEntity>();
    //    }
    //}
}
