using AutoMapper;
using Delivery.Api.BL.Facades;
using Delivery.Api.DAL.Common.Repositories;
using Delivery.Common.Enums;
using Delivery.Common.Models.Dish;
using Delivery.Common.Models.Order;
using Delivery.Common.Models.OrderDish;
using Moq;

namespace Delivery.Api.BL.UnitTests
{
    public class OrderFacadeTests
    {
        private static OrderFacade GetFacadeWithForbiddenDependencyCalls()
        {
            var repository = new Mock<IOrderRepository>(MockBehavior.Strict).Object;
            var mapper = new Mock<IMapper>(MockBehavior.Strict).Object;
            var facade = new OrderFacade(repository, mapper);
            return facade;
        }

        [Fact]
        public void Delete_Calls_Correct_Method_On_Repository()
        {
            var repositoryMock = new Mock<IOrderRepository>(MockBehavior.Strict);
            repositoryMock.Setup(orderRepository => orderRepository.Remove(It.IsAny<Guid>()));

            var repository = repositoryMock.Object;
            var mapper = new Mock<IMapper>(MockBehavior.Strict).Object;
            var facade = new OrderFacade(repository, mapper);

            var itemId = Guid.NewGuid();

            facade.Delete(itemId);

            repositoryMock.Verify(orderRepository => orderRepository.Remove(itemId));
        }

        /*[Fact]
        public void MergeDishAmounts_Merges_Order_With_Multiple_DishAmounts_Of_Same_Dish()
        {
            var facade = GetFacadeWithForbiddenDependencyCalls();

            var restaurantId  = Guid.NewGuid();
            var orderId  = Guid.NewGuid();
            var order = new OrderCreateModel()
            {
                Id = orderId,
                Address = "TestAddress",
                DeliveryTime = TimeSpan.FromMinutes(40),
                DishAmounts = new List<OrderDetailDishModel>{},
                Note = "note",
                RestaurantId = restaurantId,
                State = OrderStates.Created
            };

            facade.MergeDishAmounts(order);

            var mergedDish = Assert.Single(order.DishAmounts);
            Assert.Equal(3, mergedDish.Amount);
            Assert.Equal(mergedDishId, mergedDish.Dish.Id);
        }

        [Fact]
        public void MergeDishAmounts_Does_Not_Fail_When_Order_Has_No_Dishes()
        {
            var facade = GetFacadeWithForbiddenDependencyCalls();
            var order = new OrderDetailModel()
            {
                Id = Guid.NewGuid(),
                Address = "Address",
                DeliveryTime = TimeSpan.FromHours(1),
                Note = "Note",
                State = OrderStates.Created,
                RestaurantId = Guid.NewGuid(),
                DishAmounts = new List<OrderDetailDishModel>() { }
            };

            facade.MergeDishAmounts(order);

            Assert.Empty(order.DishAmounts);
        }*/
    }
}
