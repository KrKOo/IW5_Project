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

        /*TODO: Zjistit jestli se to vůbec dá testovat, podle mě toto jde přes databázy a nee repozitáře..Prodiskutovat!!
        [Fact]
        public void Restaurant_Create()
        {
            var facade = GetFacadeWithForbiddenDependencyCalls();

            var restaurantId = Guid.NewGuid();
            var restaurant = new RestaurantCreateModel()
            {
                Id = restaurantId,
                Name = "TestRestaurantName",
                Description = "Test",
                Address = "TestAddress",
                Latitude = 24.55488,
                Longitude = -81.15436,
                LogoUrl = "logo" 
            };
            
            facade.Create(restaurant);
            var restaurantFromDb = facade.GetById(restaurantId);
                
            Assert.Empty(restaurantFromDb.Orders);
        }*/
    }
}
