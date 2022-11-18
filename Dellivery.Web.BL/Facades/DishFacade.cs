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
        private readonly DishClient dishClient;

        // TODO: fix RepositoryBase

        /*public DishFacade(
            DishClient dishClient,
            DishRepository repository, 
            IMapper mapper, LocalDbOptions localDbOptions) 
            : base(repository, mapper, localDbOptions)
        {
            this.dishClient = dishClient;
        }*/

        public override Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task<DishDetailModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        protected override Task<Guid> SaveToApiAsync(DishDetailModel data)
        {
            throw new NotImplementedException();
        }
    }
}
