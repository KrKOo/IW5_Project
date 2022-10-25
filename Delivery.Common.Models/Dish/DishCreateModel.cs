namespace Delivery.Common.Models.Dish
{
    public record DishCreateModel : DishBaseModel
    {
        public Guid? RestaurantId { get; set; }
    }
}
