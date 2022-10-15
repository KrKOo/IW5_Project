﻿using System;
using Delivery.Common.Enums;
using AutoMapper;

namespace Delivery.Api.DAL.Common.Entities
{
    public record DishEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid RestaurantId { get; set; }
        public RestaurantEntity? Restaurant { get; set; }
        public string? ImageUrl { get; set; }
        public ICollection<Allergen> Allergens { get; set; } = new List<Allergen>();

        public DishEntity(Guid id, string name, string description, double price, Guid restaurantId, string? imageUrl = null) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            RestaurantId = restaurantId;
            ImageUrl = imageUrl;
        }
    }

    public class DishEntityMapperProfile : Profile
    {
        public DishEntityMapperProfile()
        {
            CreateMap<DishEntity, DishEntity>();
        }
    }
}
