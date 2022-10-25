using Delivery.Common.Models.Dish;

namespace Delivery.Common.Models.OrderDish
{
    public record OrderDishDetailModel : OrderDishBaseModel
    {
        public required DishListModel Dish { get; set; }
    }
}
