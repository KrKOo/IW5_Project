using System;
using System.ComponentModel.DataAnnotations;
using Delivery.Common.Models.Resources;
using Delivery.Common.Models.Restaurant;

namespace Delivery.Common.Models.Dish
{
    public record DishDetailModel : IWithId
    {
        public required Guid Id { get; init; }

        [Required(ErrorMessageResourceName = nameof(DishDetailModelResources.Name_Required_ErrorMessage), ErrorMessageResourceType = typeof(DishDetailModelResources))]
        public required string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(DishDetailModelResources.Description_Required_ErrorMessage), ErrorMessageResourceType = typeof(DishDetailModelResources))]
        [MinLength(10, ErrorMessageResourceName = nameof(DishDetailModelResources.Description_MinLength_ErrorMessage), ErrorMessageResourceType = typeof(DishDetailModelResources))]

        public required string Description { get; set; }

        [Required(ErrorMessageResourceName = nameof(DishDetailModelResources.Price_Required_ErrorMessage), ErrorMessageResourceType = typeof(DishDetailModelResources))]
        public required double Price { get; set; }

        [Required(ErrorMessageResourceName = nameof(DishDetailModelResources.Allergens_Required_ErrorMessage), ErrorMessageResourceType = typeof(DishDetailModelResources))]
        public required string Allergens { get; set; }

        public string? ImageUrl { get; set; }

        public RestaurantDetailModel? Restaurant { get; set; }
    }
}
