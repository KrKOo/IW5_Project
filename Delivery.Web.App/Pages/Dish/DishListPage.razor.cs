using System.Collections.Generic;
using Delivery.Common.Models.Dish;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App.Pages
{
    public partial class DishListPage
    {
        [Inject]
        private DishFacade DishFacade { get; set; } = null!;

        private ICollection<DishListModel> Dishes { get; set; } = new List<DishListModel>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Dishes = await DishFacade.GetAllAsync();
        }
    }
}
