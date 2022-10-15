using System;
using System.Collections.Generic;
using System.Linq;
using Delivery.Api.DAL.Common.Entities;
using Delivery.Common.Enums;
using Xunit;

namespace Delivery.API.DAL.IntegrationTests;

public class DishRepositoryTests
{
    public DishRepositoryTests()
    {
        _dbFixture = new InMemoryDatabaseFixture();
    }

    private readonly IDatabaseFixture _dbFixture;

    [Fact]
    public void GetAll_Dishes()
    {
        //arrange
        var dishRepository = _dbFixture.GetDishRepository();

        //act
        var dishesCount = dishRepository.GetAll().Count();

        //assert
        Assert.Equal(3, dishesCount);
    }
    
    [Fact]
    public void Update_Dish()
    {
        //arrange
        var dishRepository = _dbFixture.GetDishRepository();

        var dishId = _dbFixture.DishGuids[0];
        var dish = _dbFixture.GetDishDirectly(dishId);
        
        //act
        dish.Price = 4.70;
        dish.Description = "Extra salam";
        dishRepository.Update(dish);

        //assert
        var dishFromDb = _dbFixture.GetDishDirectly(dishId);
        Assert.NotNull(dishFromDb);
        Assert.Equal(dish.Price, dishFromDb.Price);
    }
    
    [Fact]
    public void Insert_Dish()
    {
        //arrange
        var dishRepository = _dbFixture.GetDishRepository();

        var allergenList = new List<Allergen>() { Allergen.Eggs, Allergen.Milk };
        
        var dishId = Guid.NewGuid();
        var dish = new DishEntity(dishId, "Pancakes", "Chocolate", 2.80, _dbFixture.RestaurantGuids[0])
        {
            Allergens = allergenList
        };
        
        //act
        dishRepository.Insert(dish);

        //assert
        var dishFromDb = _dbFixture.GetDishDirectly(dishId);
        Assert.NotNull(dishFromDb);
        Assert.Equal(allergenList.Count, dishFromDb.Allergens.Count);
    }
    
    [Fact]
    public void Insert_Than_Remove_Dish()
    {
        //arrange
        var dishRepository = _dbFixture.GetDishRepository();

        var dishId = Guid.NewGuid();
        var dish = new DishEntity(dishId, "Cake", "Chocolate", 9.80, _dbFixture.RestaurantGuids[0]);
        
        //act
        dishRepository.Insert(dish);

        //assert
        var dishFromDb = _dbFixture.GetDishDirectly(dishId);
        Assert.NotNull(dishFromDb);
        Assert.Equal(dish.Name, dishFromDb.Name);
        
        //act
        dishRepository.Remove(dishId);
        
        //assert
        var removedDishFromDb = _dbFixture.GetOrderDirectly(dishId);
        Assert.Null(removedDishFromDb);
    }
    
    [Fact]
    public void Exist_Dish()
    {
        //arrange
        var dishRepository = _dbFixture.GetDishRepository();
        var dishId = _dbFixture.DishGuids[0];
        var unknownId = Guid.NewGuid();

        //assert
        Assert.True(dishRepository.Exists(dishId));
        Assert.False(dishRepository.Exists(unknownId));
    }

    
    //    [Fact]
    //    public void Update_Also_Updates_DishAmount()
    //    {
    //        //arrange
    //        var orderRepository = _dbFixture.GetOrderRepository();

    //        var orderId = _dbFixture.OrderGuids[0];
    //        var order = _dbFixture.GetOrderDirectly(orderId);
    //        var dishAmount = order.DishAmounts.First();

    //        //act
    //        dishAmount.Amount = 8;
    //        //order.State = OrderStates.Delivered;  TODO!!
    //        orderRepository.Update(order);

    //        //assert
    //        var orderFromDb = _dbFixture.GetOrderDirectly(orderId);
    //        Assert.NotNull(orderFromDb);
    //        //Assert.Equal(OrderStates.Delivered, orderFromDb.State);  TODO!!
    //        var dishAmountFromDb = orderFromDb.DishAmounts.First();
    //        Assert.Equal(dishAmount.Id, dishAmountFromDb.Id);
    //        Assert.Equal(8, dishAmountFromDb.Amount);
    //    }
    
    //    [Fact]
    //    public void Insert_Then_Remove_Order()
    //    {
    //        //arrange
    //        var orderRepository = _dbFixture.GetOrderRepository();

    //        var dishAmountId = Guid.NewGuid();
    //        var orderId = Guid.NewGuid();
    //        var deliveryTime = TimeSpan.FromMinutes(43);
    //        var newOrder = new OrderEntity(orderId, "542 East Road", deliveryTime, "TestOrder", OrderStates.Accepted, _dbFixture.RestaurantGuids[0])
    //        {
    //            DishAmounts = new List<DishAmountEntity>()
    //            {
    //                new DishAmountEntity(dishAmountId, 3, _dbFixture.DishGuids[1], orderId)
    //            }
    //        };

    //        //act
    //        orderRepository.Insert(newOrder);

    //        //assert
    //        var orderFromDb = _dbFixture.GetOrderDirectly(orderId);
    //        Assert.NotNull(orderFromDb);
    //        Assert.Equal(orderId, orderFromDb.Id);

    //        //act
    //        orderRepository.Remove(orderId);
    //        var removedOrderFromDb = _dbFixture.GetOrderDirectly(orderId);
    //        Assert.Null(removedOrderFromDb);
    //    }
    
    //    [Fact]
    //    public void Insert_Saves_Order_And_DishAmounts()
    //    {
    //        //arrange
    //        var orderRepository = _dbFixture.GetOrderRepository();

    //        var dishAmountId = Guid.NewGuid();
    //        var orderId = Guid.NewGuid();
    //        var deliveryTime = TimeSpan.FromMinutes(37);
    //        var newOrder = new OrderEntity(orderId, "542 Jewell Road", deliveryTime, "TestOrder", OrderStates.Accepted, _dbFixture.RestaurantGuids[0])
    //        {
    //            DishAmounts = new List<DishAmountEntity>()
    //            {
    //                new DishAmountEntity(dishAmountId, 3, _dbFixture.DishGuids[1], orderId)
    //            }
    //        };

    //        //act
    //        orderRepository.Insert(newOrder);

    //        //assert
    //        var order = _dbFixture.GetOrderDirectly(orderId);
    //        Assert.NotNull(order);
    //        Assert.Equal(OrderStates.Accepted, order.State);
    //        Assert.Equal(deliveryTime, order.DeliveryTime);

    //        var dishAmount = _dbFixture.GetDishAmountDirectly(dishAmountId);
    //        Assert.NotNull(dishAmount);
    //        Assert.Equal(3, dishAmount.Amount);
    //        Assert.Equal(_dbFixture.DishGuids[1], dishAmount.DishId);
    //    }
}
