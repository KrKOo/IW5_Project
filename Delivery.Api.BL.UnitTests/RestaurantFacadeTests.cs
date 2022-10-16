using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Delivery.Api.BL.Facades;
using Delivery.Api.DAL.Common.Repositories;
using Moq;

namespace Delivery.Api.BL.UnitTests
{
    public class RestaurantFacadeTests
    {
        private static RestaurantFacade GetFacadeWithForbiddenDependencyCalls()
        {
            var repository = new Mock<IRestaurantRepository>(MockBehavior.Strict).Object;
            var mapper = new Mock<IMapper>(MockBehavior.Strict).Object;
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
        public void MergeOrders_Merges_Restaurant_With_Multiple_Different_Orders()
        {
            var facade = GetFacadeWithForbiddenDependencyCalls();
        }
    }
}
