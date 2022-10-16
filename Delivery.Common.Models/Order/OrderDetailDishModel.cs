using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delivery.Common.Models.Dish;

namespace Delivery.Common.Models.Order
{
    public record OrderDetailDishModel
    {
        public required Guid Id { get; set; }

        [Range(0, int.MaxValue)]
        public required int Amount { get; set; }
        public required DishListModel Dish { get; set; }
    }
}
