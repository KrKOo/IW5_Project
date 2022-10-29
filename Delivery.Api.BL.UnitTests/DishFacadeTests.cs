using AutoMapper;
using Delivery.Api.BL.Facades;
using Delivery.Api.DAL.Common.Repositories;
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
    }
}
