using System;
using System.Collections.ObjectModel;
//using AutoMapper;
using Delivery.Common.Enums;

namespace Delivery.Api.DAL.Common.Entities
{
    public record DishAllergenEntity
    {
        public Guid? Id { get; set; }

        public Allergen Allergen { get; set; }

        public Guid DishId { get; set; }
        public DishEntity? Dish { get; set; }
        // public Collection<DishEntity> Dishes { get; set; } = new Collection<DishEntity>();

        public DishAllergenEntity() { } //TODO: Ask why is that?
        public DishAllergenEntity(Guid? id, Allergen allergen, Guid dishId)
        {
            Id = id;
            Allergen = allergen;
            DishId = dishId;
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
