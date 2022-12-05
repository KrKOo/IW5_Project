using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Common.Models;
using Delivery.Web.BL.Options;
using Delivery.Web.DAL.Repositories;
using Microsoft.Extensions.Options;

namespace Delivery.Web.BL.Facades
{
    public class RestaurantFacade : FacadeBase<RestaurantDetailModel, RestaurantListModel>
    {
        private readonly RestaurantClient apiClient;

        public RestaurantFacade(
            RestaurantClient apiClient,
            RestaurantRepository restaurantRepository,
            IMapper mapper,
            IOptions<LocalDbOptions> localDbOptions)
            : base(restaurantRepository, mapper, localDbOptions)
        {
            this.apiClient = apiClient;
        }

        //TODO přepsat funkce aby seděli na ApiClienta
        
        public override async Task<List<RestaurantDetailModel>> GetAllAsync()
        {
            var restaurantsAll = await base.GetAllAsync();

            var restaurantsFromApi = await apiClient.RestaurantGetAsync(apiVersion);
            foreach (var restaurantFromApi in restaurantsFromApi)
            {
                if (restaurantsAll.Any(r => r.Id == restaurantFromApi.Id) is false)
                {
                    restaurantsAll.Add(restaurantFromApi);
                }
            }

            return restaurantsAll;
        }

        public override async Task<RestaurantDetailModel> GetByIdAsync(Guid id)
        {
            return await apiClient.RestaurantGetAsync(id, apiVersion);
        }

        protected override async Task<Guid> SaveToApiAsync(RestaurantDetailModel data)
        {
            return await apiClient.UpsertAsync(apiVersion, data);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await apiClient.RestaurantDeleteAsync(id, apiVersion);
        }
    }
}
