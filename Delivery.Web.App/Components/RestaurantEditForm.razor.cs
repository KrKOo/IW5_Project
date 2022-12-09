using System;
using System.Threading.Tasks;
using Delivery.Common.Models.Restaurant;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App
{
    public partial class RestaurantEditForm
    {

        [Inject]
        public RestaurantFacade RestaurantFacade { get; set; } = null!;

        [Parameter]
        public Guid Id { get; init; }

        [Parameter]
        public EventCallback OnModification { get; set; }

        public RestaurantDetailModel Data { get; set; } = GetNewRestaurantModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != Guid.Empty)
            {
                Data = await RestaurantFacade.GetByIdAsync(Id);
            }

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {

        }

        public async Task Delete()
        {

        }

        private async Task NotifyOnModification()
        {
            if (OnModification.HasDelegate)
            {
                await OnModification.InvokeAsync(null);
            }
        }

        private static RestaurantDetailModel GetNewRestaurantModel()
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                LogoUrl = string.Empty,
                Address = string.Empty,
                Latitude = 0,
                Longitude = 0,
            };
    }
}
