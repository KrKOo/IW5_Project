using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Common.Enums;

namespace Delivery.Common.Models.Order
{
    public record OrderListModel : IWithId
    {
        public required Guid Id { get; init; }
        public required TimeSpan DeliveryTime { get; set; }
        public required string Note { get; set; }
        public required OrderStates State { get; set; }
        public required Guid RestaurantId { get; set; }
    }
}
