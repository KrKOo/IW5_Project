using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Common;
using Delivery.Common.BL.Facades;
using Delivery.Web.BL.Options;
using Delivery.Web.DAL.Repositories;

namespace Delivery.Web.BL.Facades
{
    public abstract class FacadeBase<TDetailModel, TListModel> : IAppFacade
        where TDetailModel : IWithId
    {
        private readonly RepositoryBase<TDetailModel> repository;
        private readonly IMapper mapper;
        private readonly LocalDbOptions localDbOptions;
        protected virtual string apiVersion => "1";
        protected virtual string culture => CultureInfo.DefaultThreadCurrentCulture?.Name ?? "cs";

        protected FacadeBase(RepositoryBase<TDetailModel> repository, IMapper mapper, LocalDbOptions localDbOptions)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.localDbOptions = localDbOptions;
        }

        public virtual async Task<List<TListModel>> GetAllAsync()
        {
            var itemsAll = new List<TListModel>();

            if(localDbOptions.isLocalDbEnabled)
            {
                itemsAll.AddRange(await GetAllFromLocalDbAsync());
            }

            return itemsAll;
        }

        protected async Task<IList<TListModel>> GetAllFromLocalDbAsync()
        {
            var itmesLocal = await repository.GetAllAsync();
            return mapper.Map<IList<TListModel>>(itmesLocal);
        }

        public abstract Task<TDetailModel> GetByIdAsync(Guid id);

        public virtual async Task SaveAsync(TDetailModel data)
        {
            try
            {
                await SaveToApiAsync(data);
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("Failed to fetch")) 
            {
                if (localDbOptions.isLocalDbEnabled)
                {
                    await repository.InsertAsync(data);
                }
            }
        }

        protected abstract Task<Guid> SaveToApiAsync(TDetailModel data);
        public abstract Task DeleteAsync(Guid id);  

        public async Task<bool> SynchronizeLocalDataAsync()
        {
            var itemsLocal = await repository.GetAllAsync();
            foreach(var item in itemsLocal)
            {
                await SaveToApiAsync(item);
                await repository.RemoveAsync(item.Id);
            }

            return itemsLocal.Any();
        }
    }
}
