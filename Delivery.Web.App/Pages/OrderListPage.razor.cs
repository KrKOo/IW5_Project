using Delivery.Common.Models.Order;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App.Pages
{
    public partial class OrderListPage
    {
        [Inject]
        private OrderFacade orderFacade { get; set; } = null!;

        private ICollection<OrderListModel> Orders { get; set; } = new List<OrderListModel>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Orders = await orderFacade.GetAllAsync();
        }
    }
}
