using Delivery.Common.Models.Order;
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

        public OrderDetailModel Data { get; set; } = GetNewOrderModel();

        protected override async Task OnInitializedAsync()
        {
            if(Id != Guid.Empty)
            {
                Data = await orderFacade.GetByIdAsync(Id);
            }

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {
            //await orderFacade.SaveAsync(createModel);
            Data = GetNewOrderModel();
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

        private static OrderDetailModel GetNewOrderModel()
            => new()
            {
                Address = String.Empty,
                DeliveryTime = TimeSpan.MinValue,
                Note = String.Empty,
                State = Common.Enums.OrderState.Created
            };
    }
}
