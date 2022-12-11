using Delivery.Common.Models.Order;
using Delivery.Common.Models.OrderDish;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App.Pages
{
    public partial class OrderDetailPage
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;

        [Inject]
        public OrderFacade orderFacade { get; set; } = null!;

        private OrderDetailModel Order { get; set; } = GetNewOrderModel();
        
        public ICollection<OrderDishDetailModel> DishAmounts { get; set; } = new List<OrderDishDetailModel>();  

        [Parameter]
        public Guid Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Order = await orderFacade.GetByIdAsync(Id);
        }

        private static OrderDetailModel GetNewOrderModel()
            => new()
            {
                Address = String.Empty,
                DeliveryTime = TimeSpan.Zero,
                Note = String.Empty,
                State = Common.Enums.OrderState.Created
            };

        public void NavigateBack()
        {
            NavigationManager.NavigateTo("/orders");
        }
    }
}
