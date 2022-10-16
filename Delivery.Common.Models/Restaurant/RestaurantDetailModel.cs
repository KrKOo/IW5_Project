using System.ComponentModel.DataAnnotations;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.Resources;

namespace Delivery.Common.Models.Restaurant
{
    public record RestaurantDetailModel : IWithId
    {
        public required Guid Id { get; init; }

        [Required(ErrorMessageResourceName = nameof(RestaurantDetailModelResources.Name_Required_ErrorMessage), ErrorMessageResourceType = typeof(RestaurantDetailModelResources))]
        public required string Name { get; set; }

        [Required(ErrorMessageResourceName = nameof(RestaurantDetailModelResources.Description_Required_ErrorMessage), ErrorMessageResourceType = typeof(RestaurantDetailModelResources))]
        public required string Description { get; set; }

        [Required(ErrorMessageResourceName = nameof(RestaurantDetailModelResources.Address_Required_ErrorMessage), ErrorMessageResourceType = typeof(RestaurantDetailModelResources))]
        public required string Address { get; set; }

        [Required(ErrorMessageResourceName = nameof(RestaurantDetailModelResources.Gps_Required_ErrorMessage), ErrorMessageResourceType = typeof(RestaurantDetailModelResources))]
        public required string Gps { get; set; }

        [Required(ErrorMessageResourceName = nameof(RestaurantDetailModelResources.Revenue_Required_ErrorMessage), ErrorMessageResourceType = typeof(RestaurantDetailModelResources))]
        public required double Revenue { get; set; }

        public string? LogoUrl { get; set; }

        public IList<DishDetailModel> Dishes { get; set; } = new List<DishDetailModel>();
        public IList<OrderDetailModel> Orders { get; set; } = new List<OrderDetailModel>();
    }
}
