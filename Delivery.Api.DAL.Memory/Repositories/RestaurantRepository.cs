using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;

namespace Delivery.Api.DAL.Memory.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IList<DishEntity> dishes;
        private readonly IList<OrderEntity> orders;
        private readonly IList<RestaurantEntity> restaurants;
        private readonly IMapper mapper;

        public RestaurantRepository(
            Storage storage,
            IMapper mapper)
        {
            this.dishes = storage.Dishes;
            this.orders = storage.Orders;
            this.restaurants = storage.Restaurants;
            this.mapper = mapper;
        }

        public IList<RestaurantEntity> GetAll()
        {
            return restaurants;
        }

        public RestaurantEntity? GetById(Guid id)
        {
            return restaurants.SingleOrDefault(entity => entity.Id == id);
        }

        public Guid Insert(RestaurantEntity restaurant)
        {
            restaurants.Add(restaurant);
            return restaurant.Id;
        }

        public Guid? Update(RestaurantEntity entity)
        {
            var restaurantExisting = restaurants.SingleOrDefault(restaurantInStorage =>
                restaurantInStorage.Id == entity.Id);
            if (restaurantExisting != null)
            {
                mapper.Map(entity, restaurantExisting);
            }

            return restaurantExisting?.Id;
        }

        public void Remove(Guid id)
        {
            var restaurantToRemove = restaurants.Single(restaurant => restaurant.Id.Equals(id));
            restaurants.Remove(restaurantToRemove);
        }

        public bool Exists(Guid id)
        {
            return restaurants.Any(restaurant => restaurant.Id == id);
        }
        
        public List<RestaurantEntity> GetBySubstring(string substring)
        {
            return restaurants
                .Where(entity => entity.Name.Contains(substring) || entity.Description.Contains(substring) || entity.Address.Contains(substring)).ToList();
        }
    }
}
