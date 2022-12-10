﻿using System;
using System.Threading.Tasks;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.DishAllergen;
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
        public Guid RestaurantId { get; init; }

        [Parameter]
        public EventCallback OnModification { get; set; }

        public DishCreateModel Data { get; set; } = GetNewDishModel();
        
        public DishAllergenCreateModel NewDishAllergenModel { get; set; } = GetNewNewDishAllergenModel();

        public IList<Allergen> NotUsedAllergens = Enum.GetValues(typeof(Allergen)).Cast<Allergen>().ToList();

        public Allergen RemovedAllergen { get; set; } = Allergen.None;

        protected override async Task OnInitializedAsync()
        {
            if (Id != Guid.Empty)
            {
                //TODO HotFix
                var detailModel = await DishFacade.GetByIdAsync(Id);
                DishCreateModel createModel = new DishCreateModel()
                {
                    Id = detailModel.Id,
                    Name = detailModel.Name,
                    Description = detailModel.Description,
                    ImageUrl = detailModel.ImageUrl,
                    Allergens = detailModel.Allergens,
                    Price = detailModel.Price,
                    RestaurantId = detailModel.Restaurant.Id
                };
                Data = createModel;
                
                //Data = await DishFacade.GetByIdAsync(Id); //TODO This is correct
                
                foreach (var a in Data.Allergens)
                {
                    NotUsedAllergens.Remove(a);
                }
                NotUsedAllergens.Remove(Allergen.None);
            }

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {
            if (RestaurantId != Guid.Empty)
            {
                Data.RestaurantId = RestaurantId;
            }
            await DishFacade.SaveAsync(Data);
        }

        public async Task Delete()
        {
            await DishFacade.DeleteAsync(Data.Id);
        }

        private async Task NotifyOnModification()
        {
            if (OnModification.HasDelegate)
            {
                await OnModification.InvokeAsync(null);
            }
        }
        
        public void AddAllergen()
        {
            if (NewDishAllergenModel.Allergen != Allergen.None)
            {
                Allergen allergen = NewDishAllergenModel.Allergen; //TODO HotFix
                Data.Allergens.Add(allergen); //TODO Převod DishDetailModel nemá DishAllergenCreate ale jen Allergen
                NotUsedAllergens.Remove(allergen);
                NewDishAllergenModel = GetNewNewDishAllergenModel();
            }
        }
        
        public void RemoveAllergen()
        {
            if (RemovedAllergen != Allergen.None)
            {
                Allergen allergen = RemovedAllergen; //TODO HotFix
                Data.Allergens.Remove(allergen); //TODO Převod DishDetailModel nemá DishAllergenCreate ale jen Allergen
                NotUsedAllergens.Add(allergen);
                RemovedAllergen = Allergen.None;
            }
        }

        private static DishCreateModel GetNewDishModel()
            => new()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Description = string.Empty,
                ImageUrl = string.Empty,
                Allergens = new List<Allergen>(),
                Price = 0
            };
        
        private static DishAllergenCreateModel GetNewNewDishAllergenModel()
            => new()
            {
                Id = Guid.NewGuid(),
                DishId = Guid.Empty,
                Allergen = Allergen.None
            };
    }
}
