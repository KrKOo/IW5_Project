using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Common.Enums;
using Delivery.Common.Models.Resources;

namespace Delivery.Common.Models.Order
{
    public record OrderDetailModel : IWithId
    {
        public required Guid Id { get; init; }

        [Required(ErrorMessageResourceName = nameof(OrderDetailModelResources.Address_Required_ErrorMessage), ErrorMessageResourceType = typeof(OrderDetailModelResources))]
        public required string Address { get; set; }

        [Required(ErrorMessageResourceName = nameof(OrderDetailModelResources.DeliveryTime_Required_ErrorMessage), ErrorMessageResourceType = typeof(OrderDetailModelResources))]
        public required TimeSpan DeliveryTime { get; set; }

        [Required(ErrorMessageResourceName = nameof(OrderDetailModelResources.Note_Required_ErrorMessage), ErrorMessageResourceType = typeof(OrderDetailModelResources))]
        public required string Note { get; set; }

        [Required(ErrorMessageResourceName = nameof(OrderDetailModelResources.State_Required_ErrorMessage), ErrorMessageResourceType = typeof(OrderDetailModelResources))]
        public required OrderStates State { get; set; }

        public required Guid RestaurantId { get; set; }

        public IList<OrderDetailDishModel> DishAmounts { get; set; } = new List<OrderDetailDishModel>();
    }
}
