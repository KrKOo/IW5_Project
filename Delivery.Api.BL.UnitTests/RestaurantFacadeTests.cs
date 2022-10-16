using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Api.BL.Facades;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.Restaurant;
using Moq;

namespace Delivery.Api.BL.UnitTests
{
    public class RestaurantFacadeTests
    {
        private static RestaurantFacade GetFacadeWithForbiddenDependencyCalls()
        {
            var repository = new Mock<IRestaurantRepository>(MockBehavior.Loose).Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new RestaurantFacade(repository, mapper);
            return facade;
        }

        [Fact]
        public void Delete_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IRestaurantRepository>(MockBehavior.Strict);
            repositoryMock.Setup(restaurantRepository => restaurantRepository.Remove(It.IsAny<Guid>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Strict).Object;
            var facade = new RestaurantFacade(repository, mapper);

            var itemId = Guid.NewGuid();

            facade.Delete(itemId);

            repositoryMock.Verify(restaurantRepository => restaurantRepository.Remove(itemId));
        }

        [Fact]
        public void Restaurant_With_Multiple_Different_Orders()
        {
            var facade = GetFacadeWithForbiddenDependencyCalls();

            var orderId1 = Guid.NewGuid();
            var orderId2 = Guid.NewGuid();
            var restaurantId = Guid.NewGuid();
            var restaurant = new RestaurantDetailModel()
            {
                Id = restaurantId,
                Name = "U Globusu",
                Description = "Restauracia ceskej kuchyne",
                Address = "Niekde v Brne",
                Gps = "40.741895,-73.989308",
                Revenue = 65000.00,
                Orders = new List<OrderDetailModel>()
                {
                    new OrderDetailModel
                    {
                        Id = orderId1,
                        Address = "Purkynova 95",
                        DeliveryTime = TimeSpan.FromMinutes(30),
                        Note = "Blok B03",
                        State = OrderStates.Accepted,
                        RestaurantId = restaurantId
                    },
                    new OrderDetailModel
                    {
                        Id = orderId2,
                        Address = "Purkynova 93",
                        DeliveryTime = TimeSpan.FromMinutes(35),
                        Note = "Blok B06",
                        State = OrderStates.Created,
                        RestaurantId = restaurantId
                    }
                }
            };

            facade.Create(restaurant);

            Assert.NotEmpty(restaurant.Orders);
        }

        [Fact]
        public void Does_Not_Fail_When_Restaurant_Has_No_Orders()
        {
            var facade = GetFacadeWithForbiddenDependencyCalls();
            var restaurant = new RestaurantDetailModel()
            {
                Id = Guid.NewGuid(),
                Name = "Nepal",
                Description = "Nepalska restauracia",
                Address = "Nad FITom",
                Gps = "Nad FITom",
                Revenue = 10.00,
                Orders = new List<OrderDetailModel>() { }
            };

            facade.Create(restaurant);

            Assert.Empty(restaurant.Orders);
        }

        [Fact]
        public void Restaurant_With_Multiple_Different_Dishes()
        {
            var facade = GetFacadeWithForbiddenDependencyCalls();

            var dishId1 = Guid.NewGuid();
            var dishId2 = Guid.NewGuid();
            var restaurantId = Guid.NewGuid();
            var restaurant = new RestaurantDetailModel()
            {
                Id = restaurantId,
                Name = "Buddha",
                Description = "Indicka restauracia",
                Address = "Namestie slobody",
                Gps = "40.741895,-73.989308",
                Revenue = 75000.00,
                Dishes = new List<DishDetailModel>()
                {
                    new DishDetailModel
                    {
                        Id = dishId1,
                        Name = "Chicken Tikka Masala",
                        Description = "Tradicna stredne paliva omacka",
                        Price = 135.00,
                        Allergens = "Ziadne"
                    },
                    new DishDetailModel
                    {
                        Id = dishId2,
                        Name = "Jalfrezi",
                        Description = "Tradicna velmi paliva omacka",
                        Price = 135.00,
                        Allergens = "ziadne"
                    }
                }
            };

            facade.Create(restaurant);

            Assert.NotEmpty(restaurant.Dishes);
        }

        [Fact]
        public void Does_Not_Fail_When_Restaurant_Has_No_Dishes()
        {
            var facade = GetFacadeWithForbiddenDependencyCalls();
            var restaurant = new RestaurantDetailModel()
            {
                Id = Guid.NewGuid(),
                Name = "Seafood restauracia",
                Description = "Restauracia s morskymi plodmi",
                Address = "404",
                Gps = "NaN",
                Revenue = 0.01,
                Dishes = new List<DishDetailModel>() { }
            };

            facade.Create(restaurant);

            Assert.Empty(restaurant.Dishes);
        }
    }
}
