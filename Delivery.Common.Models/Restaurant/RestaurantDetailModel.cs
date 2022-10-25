using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Order;

namespace Delivery.Common.Models.Restaurant
{
    public record RestaurantDetailModel : RestaurantBaseModel
    {
        public IList<DishListModel> Dishes { get; set; } = new List<DishListModel>();
        public IList<OrderListModel> Orders { get; set; } = new List<OrderListModel>();
    }
}
