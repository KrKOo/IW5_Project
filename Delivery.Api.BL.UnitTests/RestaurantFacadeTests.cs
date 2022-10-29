using AutoMapper;
using Delivery.Api.BL.Facades;
using Delivery.Api.DAL.Common.Repositories;
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
    }
}
