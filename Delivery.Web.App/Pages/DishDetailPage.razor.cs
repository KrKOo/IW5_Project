using System;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App.Pages
{
    public partial class DishDetailPage
    {
        [Inject]
        public DishFacade DishFacade { get; set; } = null!;

        public DishDetailModel Dish { get; set; } = GetNewDishModel();


        [Parameter]
        public Guid Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Dish = await DishFacade.GetByIdAsync(Id);
        }

        private static DishDetailModel GetNewDishModel()
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                ImageUrl = string.Empty,
                Price = 0,
                Allergens = new List<Allergen>(),
            };
    }
}
