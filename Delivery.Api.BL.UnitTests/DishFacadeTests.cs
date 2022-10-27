using System.Diagnostics;
using AutoMapper;
using Delivery.Api.BL.Facades;
using Delivery.Api.BL.UnitTests.Seeds;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Models.Dish;
using Moq;

namespace Delivery.Api.BL.UnitTests
{
    public class DishFacadeTests
    {
        // TODO: prepisat testy aby presli s mockbehavior strict
        // Zatial prejdu s loose
        // zistis ako nastavit rovnaky mock setup pre volane metody
        // Prepisat do prace s repositarmi
        private static DishFacade GetFacadeWithForbiddenDependencyCalls()
        {
            var repository = new Mock<IDishRepository>(MockBehavior.Loose).Object;
            var mapper = new Mock<IMapper>(MockBehavior.Loose).Object;
            var facade = new DishFacade(repository, mapper);
            return facade;
        }

        [Fact]
        public void Delete_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IDishRepository>(MockBehavior.Strict);
            repositoryMock.Setup(dishRepository => dishRepository.Remove(It.IsAny<Guid>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Strict).Object;
            var facade = new DishFacade(repository, mapper);

            var itemId = Guid.NewGuid();

            facade.Delete(itemId);

            repositoryMock.Verify(dishRepository => dishRepository.Remove(itemId)); 
        }
        
        /*TODO: Zjistit jestli se to vůbec dá testovat, podle mě toto jde přes databázy a nee repozitáře..Prodiskutovat!!
        [Fact]
        public void Create_Calls_Correct_Method_On_Repository()
        {
            var facade = GetFacadeWithForbiddenDependencyCalls();
            
            var dishId = Guid.NewGuid();
            var dish = DishModelSeeds.DishSeeds[0];

            facade.Create(dish);
            var dishFromDb = facade.GetById(dishId);
            
            Assert.NotNull(dishFromDb);
            Assert.Equal(dish.Name, dishFromDb.Name);
        }

        [Fact]
        public void Update_Calls_Correct_Method_On_Repository()
        {
            var facade = GetFacadeWithForbiddenDependencyCalls();

            var updatedName = "UpdatedName";
            var dishId = Guid.NewGuid();
            var dish = DishModelSeeds.DishSeeds[1];

            facade.Create(dish);

            dish.Name = updatedName;

            facade.Update(dish);

            var dishFromDb = facade.GetById(dishId);
            Assert.Equal(updatedName, dishFromDb.Name);
        }*/
    }
}
