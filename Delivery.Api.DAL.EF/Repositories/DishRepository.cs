using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Api.DAL.EF.Repositories
{
    public class DishRepository : RepositoryBase<DishEntity>, IDishRepository
    {
        public DishRepository(DeliveryDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
