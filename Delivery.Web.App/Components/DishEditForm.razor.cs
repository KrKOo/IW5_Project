using System;
using System.Collections;
using System.Threading.Tasks;
using Delivery.Common.Enums;

using Delivery.Common.Models.Dish;
using Delivery.Web.BL;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App
{
    public partial class DishEditForm
    {
        [Inject]
        public DishFacade DishFacade { get; set; } = null!;

        [Parameter]
        public Guid Id { get; init; }

        [Parameter]
        public EventCallback OnModification { get; set; }

        public DishCreateModel Data { get; set; } = GetNewDishModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != Guid.Empty)
            {
                Data = await DishFacade.GetByIdAsync(Id);
            }

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {
            await DishFacade.SaveAsync(Data);
            Data = GetNewDishModel();
            await NotifyOnModification();
        }

        public async Task Delete()
        {
            await DishFacade.DeleteAsync(Id);
            await NotifyOnModification();
        }

        private async Task NotifyOnModification()
        {
            if (OnModification.HasDelegate)
            {
                await OnModification.InvokeAsync(null);
            }
        }

        private static DishCreateModel GetNewDishModel()
        {
            return new()
            {
                //TODO Zkontrolovat!

                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                Price = 0,
                Allergens = new List<Allergen>(),
                ImageUrl = string.Empty,
                RestaurantId = null,
            };
        }
    }
}
