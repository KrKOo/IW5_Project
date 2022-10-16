﻿using Delivery.Common.BL.Facades;
using Delivery.Common.Models.Restaurant;

namespace Delivery.Api.BL.Facades
{
    public interface IRestaurantFacade : IAppFacade
    {
        List<RestaurantListModel> GetAll();
        RestaurantDetailModel? GetById(Guid id);
        Guid CreateOrUpdate(RestaurantDetailModel restaurantModel);
        Guid Create(RestaurantDetailModel restaurantModel);
        Guid? Update(RestaurantDetailModel restaurantModel);
        void Delete(Guid id);
    }
}