﻿using System;
using System.Threading.Tasks;
using Delivery.Common.Enums;

using Delivery.Common.Models.Dish;
using Delivery.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace Delivery.Web.App
{
    public partial class DishEditForm
    {
        //TODO add RestaurantId parameter for create event
        [Inject]
        public DishFacade DishFacade { get; set; } = null!;

        [Parameter]
        public Guid Id { get; init; }

        [Parameter]
        public EventCallback OnModification { get; set; }

        public DishDetailModel Data { get; set; } = GetNewDishModel();

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

        private static DishDetailModel GetNewDishModel()
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                ImageUrl = string.Empty,
                Allergens = new List<Allergen>(),
                Price = 0
            };
    }
}
