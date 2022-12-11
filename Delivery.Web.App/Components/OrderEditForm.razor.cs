using Delivery.Common.Enums;
using Delivery.Common.Models;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.OrderDish;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App
{
    public partial class OrderEditForm
    {
        [Inject]
        private OrderFacade orderFacade { get; set; } = null!;

        [Inject]
        private DishFacade dishFacade { get; set;} = null!; 

        [Parameter]
        public Guid Id { get; init; }

        [Parameter]
        public EventCallback OnModification { get; set; }

        [Parameter]
        public Guid RestaurantId { get; set; }

        [Parameter]
        public IList<DishListModel> RestaurantDishes { get; set; } = new List<DishListModel>();

        public Guid SelectedDishId { get; set; }

        private OrderCreateModel Data { get; set; } = GetNewOrderModel();

        private OrderDishCreateModel NewDishModel { get; set; } = GetNewOrderDishModel();

        

        protected override async Task OnInitializedAsync()
        {
            if(Id != Guid.Empty)
            {
                OrderDetailModel orderDetail = await orderFacade.GetByIdAsync(Id);

                // RestId, DishId, amount
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
            if (RestaurantId != Guid.Empty)
            {
                Data.RestaurantId = RestaurantId;
            }

            await orderFacade.SaveAsync(Data);
            await NotifyOnModification();
        }

        public async Task Delete()
        {
            await orderFacade.DeleteAsync(Id);
            await NotifyOnModification();
        }

        public void DeleteDish(OrderDishCreateModel dish)
        {
            var dishIndex = Data.DishAmounts.IndexOf(dish);
            Data.DishAmounts.RemoveAt(dishIndex);
        }

        public void AddDish()
        {
            Data.DishAmounts.Add(NewDishModel);
            NewDishModel = GetNewOrderDishModel();
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

        private static OrderDishCreateModel GetNewOrderDishModel()
            => new()
            {
                OrderId = Guid.Empty,
                DishId = Guid.Empty,
                Amount = 0
            };
    }
}
