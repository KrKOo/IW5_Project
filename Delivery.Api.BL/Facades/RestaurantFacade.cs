using AutoMapper;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.Restaurant;

namespace Delivery.Api.BL.Facades
{
    public class RestaurantFacade : IRestaurantFacade
    {
        private readonly IRestaurantRepository restaurantRepository;
        private readonly IMapper mapper;

        public RestaurantFacade(IRestaurantRepository restaurantRepository, IMapper mapper)
        {
            this.restaurantRepository = restaurantRepository;
            this.mapper = mapper;   
        }

        public Guid Create(RestaurantDetailModel restaurantModel)
        {
            //MergeOrders(restaurantModel);
            //MergeDishes(restaurantModel);
            var restaurantEntity = mapper.Map<RestaurantEntity>(restaurantModel);
            return restaurantRepository.Insert(restaurantEntity);
        }

        public Guid CreateOrUpdate(RestaurantDetailModel restaurantModel)
        {
            return restaurantRepository.Exists(restaurantModel.Id)
                ? Update(restaurantModel)!.Value
                : Create(restaurantModel);
        }

        public void Delete(Guid id)
        {
           restaurantRepository.Remove(id);
        }

        public List<RestaurantListModel> GetAll()
        {
            var restaurantEntities = restaurantRepository.GetAll();
            return mapper.Map<List<RestaurantListModel>>(restaurantEntities);
        }

        public RestaurantDetailModel? GetById(Guid id)
        {
            var restaurantEntity = restaurantRepository.GetById(id);
            return mapper.Map<RestaurantDetailModel>(restaurantEntity);
        }

        public Guid? Update(RestaurantDetailModel restaurantModel)
        {
            //MergeOrders(restaurantModel);
            //MergeDishes(restaurantModel);

            var restaurantEntity = mapper.Map<RestaurantEntity>(restaurantModel);
            restaurantEntity.Orders = restaurantModel.Orders.Select(t =>
                new OrderEntity
                (
                    t.Id,
                    t.Address,
                    t.DeliveryTime,
                    t.Note,
                    restaurantEntity.Id
                )).ToList();

            restaurantEntity.Dishes = restaurantModel.Dishes.Select(t =>
                new DishEntity(
                        t.Id,
                        t.Name,
                        t.Description,
                        t.Price,
                        restaurantEntity.Id,
                        t.ImageUrl
                    )).ToList();

            var result = restaurantRepository.Update(restaurantEntity);
            return result;
        }

        /*public void MergeOrders(RestaurantDetailModel restaurant)
        {
            var result = new List<OrderDetailModel>();
            var ordersGroups = restaurant.Orders.GroupBy(t => $"{t.Id}");

            foreach(var orderGroup in ordersGroups)
            {
                var order = orderGroup.First();
                if(order.RestaurantId == restaurant.Id)
                {
                    result.Add(order);
                }
            }

            restaurant.Orders = result;
        }

        public void MergeDishes(RestaurantDetailModel restaurant)
        {
            var result = new List<DishDetailModel>();
            var dishesGroups = restaurant.Dishes.GroupBy(t => $"{t.Id}");

            foreach(var dishGroup in dishesGroups)
            {
                var dish = dishGroup.First();
                if(dish.Restaurant == restaurant)
                {
                    result.Add(dish);
                }
            }

            restaurant.Dishes = result;
        }*/
    }
}
