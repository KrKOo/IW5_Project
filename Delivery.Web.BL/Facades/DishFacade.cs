using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Web.BL.Options;
using Delivery.Web.DAL.Repositories;
using Microsoft.Extensions.Options;

namespace Delivery.Web.BL.Facades
{
    public class DishFacade : FacadeBase<DishDetailModel, DishListModel>
    {
        private readonly DishClient apiClient;

        public DishFacade(
            DishClient apiClient,
            DishRepository dishRepository,
            IMapper mapper,
            IOptions<LocalDbOptions> localDbOptions)
            : base(dishRepository, mapper, localDbOptions)
        {
            this.apiClient = apiClient;
        }
        
        //TODO přepsat funkce aby seděli na ApiClienta

        public override async Task<List<DishListModel>> GetAllAsync()
        {
            var dishesAll = await base.GetAllAsync();

            var dishesFromApi = await apiClient.GetAsync(apiVersion);
            dishesAll.AddRange(dishesFromApi);

            return dishesAll;
        }

        public override async Task<DishDetailModel> GetByIdAsync(Guid id)
        {
            return await apiClient.GetAsync(id, apiVersion);
        }

        protected override async Task<Guid> SaveToApiAsync(DishDetailModel data)
        {
            return await apiClient.UpsertAsync(apiVersion, data);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await apiClient.DishDeleteAsync(id, apiVersion);
        }
    }
}
