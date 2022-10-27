using Delivery.Common.Enums;

namespace Delivery.Common.Models.OrderDish
{
    public record DishAllergenCreateModel : DishAllergenBaseModel
    {
        public required Guid DishId { get; set; }
        public required Allergen Allergen { get; set; }
    }
}
