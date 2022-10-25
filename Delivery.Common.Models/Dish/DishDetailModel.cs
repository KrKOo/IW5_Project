using Delivery.Common.Models.Restaurant;

namespace Delivery.Common.Models.Dish
{
    public record DishDetailModel : DishBaseModel
    {
        public required Guid Id { get; init; }
        public RestaurantListModel? Restaurant { get; set; }
    }
}
