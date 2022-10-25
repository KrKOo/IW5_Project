namespace Delivery.Common.Models.OrderDish
{
    public record OrderDishCreateModel : OrderDishBaseModel
    {
        public required Guid DishId { get; set; }
    }
}
