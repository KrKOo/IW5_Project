﻿using System;
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

        public RestaurantCreateModel Data { get; set; } = GetNewRestaurantModel();

        protected override async Task OnInitializedAsync()
        {
            if (Id != Guid.Empty)
            {
                //TODO This is DEBUG
                var detailModel = await RestaurantFacade.GetByIdAsync(Id);
                RestaurantCreateModel createModel = new RestaurantCreateModel()
                {
                    Id = detailModel.Id,
                    Name = detailModel.Name,
                    Description = detailModel.Description,
                    LogoUrl = detailModel.LogoUrl,
                    Address = detailModel.Address,
                    Latitude = detailModel.Latitude,
                    Longitude = detailModel.Longitude,
                };
                Data = createModel;
                
                //Data = await RestaurantFacade.GetByIdAsync(Id); //TODO This is correct
            }

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {
            await RestaurantFacade.SaveAsync(Data);
        }

        public async Task Delete()
        {
            await RestaurantFacade.DeleteAsync(Data.Id);
        }

        private async Task NotifyOnModification()
        {
            if (OnModification.HasDelegate)
            {
                await OnModification.InvokeAsync(null);
            }
        }

        private static RestaurantCreateModel GetNewRestaurantModel()
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
