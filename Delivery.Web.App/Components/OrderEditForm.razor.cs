using Delivery.Common.Enums;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.OrderDish;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App
{
    public partial class OrderEditForm
    {
        [Inject]
        public OrderFacade orderFacade { get; set; } = null!;

        [Parameter]
        public Guid Id { get; init; }

        [Parameter]
        public EventCallback OnModification { get; set; }

        public OrderCreateModel Data { get; set; } = GetNewOrderModel();

        protected override async Task OnInitializedAsync()
        {
            if(Id != Guid.Empty)
            {
                OrderDetailModel orderDetail = await orderFacade.GetByIdAsync(Id);
                OrderCreateModel orderCreate = new OrderCreateModel()
                {
                    Id = orderDetail.Id,
                    Address = orderDetail.Address,
                    DeliveryTime = orderDetail.DeliveryTime,
                    Note = orderDetail.Note,
                    State = orderDetail.State,
                    RestaurantId = orderDetail.Restaurant!.Id,
                    DishAmounts = (IList<OrderDishCreateModel>)orderDetail.DishAmounts
                };

                Data = orderCreate;
            }

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {
            await orderFacade.SaveAsync(Data);
            await NotifyOnModification();
        }

        public async Task Delete()
        {
            await orderFacade.DeleteAsync(Id);
            await NotifyOnModification();
        }

        private async Task NotifyOnModification()
        {
            if (OnModification.HasDelegate)
            {
                await OnModification.InvokeAsync(null);
            }
        }

        private static OrderCreateModel GetNewOrderModel()
            => new()
            {
                Id = Guid.NewGuid(),
                Address = String.Empty,
                DeliveryTime = TimeSpan.Zero,
                Note = String.Empty,
                State = OrderState.Created,
                RestaurantId = Guid.Empty,
                DishAmounts = new List<OrderDishCreateModel>()
            };
    }
}
