using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;

namespace Delivery.Api.DAL.Memory.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly IList<DishEntity> dishes;
        private readonly IList<DishAmountEntity> dishAmounts;
        private readonly IMapper mapper;

        public DishRepository(
            Storage storage,
            IMapper mapper)
        {
            this.dishes = storage.Dishes;
            this.dishAmounts = storage.DishAmounts;
            this.mapper = mapper;
        }

        public IList<DishEntity> GetAll()
        {
            return dishes;
        }

        public DishEntity? GetById(Guid id)
        {
            return dishes.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(DishEntity dish)
        {
            dishes.Add(dish);
            return dish.Id;
        }

        public Guid? Update(DishEntity entity)
        {
            var dishExisting = dishes.SingleOrDefault(dishInStorage =>
                dishInStorage.Id == entity.Id);
            if (dishExisting != null)
            {
                mapper.Map(entity, dishExisting);
            }
            
            return dishExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var dishAmountsToRemove =
                dishAmounts.Where(dishAmount => dishAmount.DishId == id).ToList();

            for (var i = 0; i < dishAmountsToRemove.Count; i++)
            {
                var dishAmountToRemove = dishAmountsToRemove.ElementAt(i);
                dishAmounts.Remove(dishAmountToRemove);
            }

            var dishToRemove = dishes.Single(dish => dish.Id.Equals(id));
            dishes.Remove(dishToRemove);
        }

        public bool Exists(Guid id)
        {
            return dishes.Any(dish => dish.Id == id);
        }
    }
}
